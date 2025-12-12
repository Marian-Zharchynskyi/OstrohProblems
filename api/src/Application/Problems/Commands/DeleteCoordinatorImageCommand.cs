using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Application.Services.ImageService;
using Domain.Problems;
using MediatR;

namespace Application.Problems.Commands;

public record DeleteCoordinatorImageCommand : IRequest<Result<Problem, ProblemException>>
{
    public Guid ProblemId { get; init; }
    public Guid CoordinatorImageId { get; init; }
}

public class DeleteCoordinatorImageCommandHandler(
    IProblemRepository problemRepository,
    IImageService imageService) : IRequestHandler<DeleteCoordinatorImageCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(DeleteCoordinatorImageCommand request,
        CancellationToken cancellationToken)
    {
        var coordinatorImageId = new CoordinatorImageId(request.CoordinatorImageId);
        var problemId = new ProblemId(request.ProblemId);
        var existingProblem = await problemRepository.GetById(problemId, cancellationToken);

        return await existingProblem.Match(
            async problem => await HandleImageDeletion(problem, coordinatorImageId, cancellationToken),
            () => Task.FromResult<Result<Problem, ProblemException>>(
                new ProblemNotFoundException(problemId)));
    }

    private async Task<Result<Problem, ProblemException>> HandleImageDeletion(Problem problem,
        CoordinatorImageId coordinatorImageId, CancellationToken cancellationToken)
    {
        var coordinatorImage = problem.CoordinatorImages.FirstOrDefault(x => x.Id == coordinatorImageId);
        if (coordinatorImage is null)
        {
            return new CoordinatorImageNotFoundException(coordinatorImageId);
        }

        var deleteResult = await imageService.DeleteImageAsync(coordinatorImage.FilePath);

        return await deleteResult.Match<Task<Result<Problem, ProblemException>>>(
            async _ =>
            {
                problem.RemoveCoordinatorImage(coordinatorImageId);
                await problemRepository.Update(problem, cancellationToken);
                return problem;
            },
            error => Task.FromResult<Result<Problem, ProblemException>>(new ImageSaveException(problem.Id)));
    }
}
