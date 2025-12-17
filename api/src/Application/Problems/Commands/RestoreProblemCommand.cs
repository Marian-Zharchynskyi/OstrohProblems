using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Application.Services.SignalRService;
using Domain.Problems;
using MediatR;

namespace Application.Problems.Commands;

public record RestoreProblemCommand : IRequest<Result<Problem, ProblemException>>
{
    public required Guid ProblemId { get; init; }
}

public class RestoreProblemCommandHandler(
    IProblemRepository problemRepository,
    ISignalRService signalRService)
    : IRequestHandler<RestoreProblemCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(
        RestoreProblemCommand request,
        CancellationToken cancellationToken)
    {
        var problemId = new ProblemId(request.ProblemId);
        var existingProblem = await problemRepository.GetById(problemId, cancellationToken);

        return await existingProblem.Match<Task<Result<Problem, ProblemException>>>(
            async problem =>
            {
                try
                {
                    problem.UpdateStatus(ProblemStatus.New);
                    problem.ClearRejection();
                    problem.ClearCoordinator();
                    var result = await problemRepository.Update(problem, cancellationToken);

                    await signalRService.SendRefreshToAll(cancellationToken);

                    return result;
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
