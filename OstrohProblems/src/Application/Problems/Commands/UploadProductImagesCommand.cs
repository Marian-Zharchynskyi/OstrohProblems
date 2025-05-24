using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Application.Services;
using Application.Services.ImageService;
using Domain.Problems;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Problems.Commands;

public record UploadProblemImagesCommand : IRequest<Result<Problem, ProblemException>>
{
    public Guid ProblemId { get; init; }
    public IFormFileCollection ImagesFiles { get; init; }
}

public class UploadProblemImagesCommandHandler(
    IProblemRepository problemRepository,
    IImageService imageService) : IRequestHandler<UploadProblemImagesCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(UploadProblemImagesCommand request,
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
        var imageSaveResult = await imageService.SaveImagesFromFilesAsync(ImagePaths.ProblemImagesPath, imagesFiles);

        return await imageSaveResult.Match<Task<Result<Problem, ProblemException>>>(
            async imagesNames =>
            {
                var imagesEntities = new List<ProblemImage>();

                foreach (var imageName in imagesNames)
                {
                    imagesEntities.Add(ProblemImage.New(ProblemImageId.New(), problem.Id, imageName));
                }

                problem.UploadProblemImages(imagesEntities);

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