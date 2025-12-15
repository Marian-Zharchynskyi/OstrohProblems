using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Application.Services.SignalRService;
using Domain.Problems;
using MediatR;

namespace Application.Problems.Commands;

public record UpdateCurrentStateCommand : IRequest<Result<Problem, ProblemException>>
{
    public required Guid ProblemId { get; init; }
    public required string CurrentState { get; init; }
}

public class UpdateCurrentStateCommandHandler(
    IProblemRepository problemRepository,
    ISignalRService signalRService)
    : IRequestHandler<UpdateCurrentStateCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(
        UpdateCurrentStateCommand request,
        CancellationToken cancellationToken)
    {
        var problemId = new ProblemId(request.ProblemId);
        var existingProblem = await problemRepository.GetById(problemId, cancellationToken);

        return await existingProblem.Match<Task<Result<Problem, ProblemException>>>(
            async problem =>
            {
                try
                {
                    problem.UpdateCurrentState(request.CurrentState);
                    var result = await problemRepository.Update(problem, cancellationToken);

                    if (problem.CreatedById != null)
                    {
                        await signalRService.SendNotificationToUser(
                            problem.CreatedById,
                            NotificationDto.Create("status_change",
                                $"Оновлено поточний стан вашої проблеми '{problem.Title}': {request.CurrentState}",
                                problem.Id.Value.ToString(),
                                problem.Title),
                            cancellationToken);
                    }

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
