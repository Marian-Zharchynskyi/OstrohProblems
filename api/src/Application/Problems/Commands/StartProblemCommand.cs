using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Application.Services.SignalRService;
using Domain.Problems;
using MediatR;

namespace Application.Problems.Commands;

public record StartProblemCommand : IRequest<Result<Problem, ProblemException>>
{
    public required Guid ProblemId { get; init; }
    public string? CurrentState { get; init; }
}

public class StartProblemCommandHandler(
    IProblemRepository problemRepository,
    ISignalRService signalRService)
    : IRequestHandler<StartProblemCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(
        StartProblemCommand request,
        CancellationToken cancellationToken)
    {
        var problemId = new ProblemId(request.ProblemId);
        var existingProblem = await problemRepository.GetById(problemId, cancellationToken);

        return await existingProblem.Match<Task<Result<Problem, ProblemException>>>(
            async problem =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(request.CurrentState))
                    {
                        problem.UpdateCurrentState(request.CurrentState);
                    }
                    problem.UpdateStatus(ProblemStatus.InProgress);
                    var result = await problemRepository.Update(problem, cancellationToken);

                    if (problem.CreatedById != null)
                    {
                        await signalRService.SendNotificationToUser(
                            problem.CreatedById,
                            NotificationDto.Create("status_change",
                                $"Роботу над вашою проблемою '{problem.Title}' розпочато!",
                                problem.Id.Value.ToString(),
                                problem.Title),
                            cancellationToken);
                    }

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
