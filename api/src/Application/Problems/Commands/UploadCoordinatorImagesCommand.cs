using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Application.Services.ImageService;
using Domain.Problems;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Problems.Commands;

public record UploadCoordinatorImagesCommand : IRequest<Result<Problem, ProblemException>>
{
    public const int MaxImagesCount = 6;

    public Guid ProblemId { get; init; }
    public IFormFileCollection ImagesFiles { get; init; }
}

public class UploadCoordinatorImagesCommandHandler(
    IProblemRepository problemRepository,
    IImageService imageService) : IRequestHandler<UploadCoordinatorImagesCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(UploadCoordinatorImagesCommand request,
        CancellationToken cancellationToken)
    {
        var problemId = new ProblemId(request.ProblemId);
        var existingProblem = await problemRepository.GetById(problemId, cancellationToken);

        return await existingProblem.Match(
            async problem => await UploadImages(problem, request.ImagesFiles, cancellationToken),
            () => Task.FromResult<Result<Problem, ProblemException>>(
                new ProblemNotFoundException(problemId)));
    }

    private async Task<Result<Problem, ProblemException>> UploadImages(
        Problem problem,
        IFormFileCollection imagesFiles,
        CancellationToken cancellationToken)
    {
        var currentImagesCount = problem.CoordinatorImages.Count;
        var newImagesCount = imagesFiles.Count;

        if (currentImagesCount + newImagesCount > UploadCoordinatorImagesCommand.MaxImagesCount)
        {
            return new MaxCoordinatorImagesExceededException(problem.Id, UploadCoordinatorImagesCommand.MaxImagesCount);
        }

        var imageSaveResult = await imageService.SaveImagesFromFilesAsync(ImagePaths.CoordinatorImages, imagesFiles);

        return await imageSaveResult.Match(
            async imagesNames =>
            {
                var imagesEntities = new List<CoordinatorImage>();

                foreach (var imageName in imagesNames)
                {
                    imagesEntities.Add(CoordinatorImage.New(CoordinatorImageId.New(), problem.Id, imageName));
                }

                problem.UploadCoordinatorImages(imagesEntities);

                try
                {
                    var problemWithImages = await problemRepository.Update(problem, cancellationToken);
                    return problemWithImages;
                }
                catch (DbUpdateConcurrencyException)
                {
                    return new ProblemConcurrencyException(problem.Id);
                }
            },
            () => Task.FromResult<Result<Problem, ProblemException>>(new ImageSaveException(problem.Id)));
    }
}
