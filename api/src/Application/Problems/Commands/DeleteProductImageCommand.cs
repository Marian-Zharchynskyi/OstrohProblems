using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Application.Services;
using Application.Services.ImageService;
using Domain.Problems;
using MediatR;

namespace Application.Problems.Commands;

public record DeleteProblemImageCommand : IRequest<Result<Problem, ProblemException>>
{
    public Guid ProblemId { get; init; }
    public Guid ProblemImageId { get; init; }
}

public class DeleteProblemImageCommandHandler(
    IProblemRepository problemRepository,
    IImageService imageService) : IRequestHandler<DeleteProblemImageCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(DeleteProblemImageCommand request,
        CancellationToken cancellationToken)
    {
        var problemImageId = new ProblemImageId(request.ProblemImageId);
        var problemId = new ProblemId(request.ProblemId);
        var existingProblem = await problemRepository.GetById(problemId, cancellationToken);

        return await existingProblem.Match(
            async problem => await HandleImageDeletion(problem, problemImageId, cancellationToken),
            () => Task.FromResult<Result<Problem, ProblemException>>(
                new ProblemNotFoundException(problemId)));
    }

    private async Task<Result<Problem, ProblemException>> HandleImageDeletion(Problem problem,
        ProblemImageId problemImageId, CancellationToken cancellationToken)
    {
        var problemImage = problem.Images.FirstOrDefault(x => x.Id == problemImageId);
        if (problemImage is null)
        {
            return new ImageNotFoundException(problemImageId);
        }

        var deleteResult = await imageService.DeleteImageAsync(ImagePaths.ProblemImagesPath, problemImage.FilePath);

        return await deleteResult.Match<Task<Result<Problem, ProblemException>>>(
            async _ =>
            {
                problem.RemoveImage(problemImageId);
                await problemRepository.Update(problem, cancellationToken);
                return problem;
            },
            error => Task.FromResult<Result<Problem, ProblemException>>(new ImageSaveException(problem.Id)));
    }
}