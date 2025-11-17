using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Domain.Problems;
using MediatR;

namespace Application.Problems.Commands;

public record ConfirmProblemByUserCommand : IRequest<Result<Problem, ProblemException>>
{
    public required Guid ProblemId { get; init; }
    public required UserConfirmationStatus ConfirmationStatus { get; init; }
}

public class ConfirmProblemByUserCommandHandler(
    IProblemRepository problemRepository)
    : IRequestHandler<ConfirmProblemByUserCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(
        ConfirmProblemByUserCommand request,
        CancellationToken cancellationToken)
    {
        var problemId = new ProblemId(request.ProblemId);
        var existingProblem = await problemRepository.GetById(problemId, cancellationToken);

        return await existingProblem.Match<Task<Result<Problem, ProblemException>>>(
            async problem =>
            {
                try
                {
                    problem.SetUserConfirmation(request.ConfirmationStatus);
                    return await problemRepository.Update(problem, cancellationToken);
                }
                catch (Exception exception)
                {
                    return new ProblemUnknownException(problem.Id, exception);
                }
            },
            () => Task.FromResult<Result<Problem, ProblemException>>(
                new ProblemNotFoundException(problemId))
        );
    }
}
