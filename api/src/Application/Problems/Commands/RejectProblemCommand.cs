using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Application.Services.SignalRService;
using Domain.Identity.Users;
using Domain.Problems;
using MediatR;

namespace Application.Problems.Commands;

public record RejectProblemCommand : IRequest<Result<Problem, ProblemException>>
{
    public required Guid ProblemId { get; init; }
    public required Guid CoordinatorId { get; init; }
    public required string RejectionReason { get; init; }
}

public class RejectProblemCommandHandler(
    IProblemRepository problemRepository,
    ISignalRService signalRService)
    : IRequestHandler<RejectProblemCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(
        RejectProblemCommand request,
        CancellationToken cancellationToken)
    {
        var problemId = new ProblemId(request.ProblemId);
        var existingProblem = await problemRepository.GetById(problemId, cancellationToken);

        return await existingProblem.Match<Task<Result<Problem, ProblemException>>>(
            async problem =>
            {
                try
                {
                    problem.AssignCoordinator(new UserId(request.CoordinatorId));
                    problem.Reject(request.RejectionReason);
                    problem.UpdateStatus(ProblemStatus.Rejected);
                    var result = await problemRepository.Update(problem, cancellationToken);

                    if (problem.CreatedById != null)
                    {
                        await signalRService.SendNotificationToUser(
                            problem.CreatedById,
                            NotificationDto.Create("rejected",
                                $"Вашу проблему відхилено. Причина: {request.RejectionReason}",
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

