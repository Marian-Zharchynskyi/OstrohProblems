using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Application.Services.ImageService;
using Domain.Problems;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Problems.Commands;

public record UploadProblemImagesCommand : IRequest<Result<Problem, ProblemException>>
{
    public const int MaxImagesCount = 6;
    
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
        var currentImagesCount = problem.Images.Count;
        var newImagesCount = imagesFiles.Count;
        
        if (currentImagesCount + newImagesCount > UploadProblemImagesCommand.MaxImagesCount)
        {
            return new MaxImagesExceededException(problem.Id, UploadProblemImagesCommand.MaxImagesCount);
        }
        
        var imageSaveResult = await imageService.SaveImagesFromFilesAsync(ImagePaths.ProblemImages, imagesFiles);

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