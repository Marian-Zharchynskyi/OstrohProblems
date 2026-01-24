using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Application.Services.SignalRService;
using Domain.Identity.Users;
using Domain.Problems;
using MediatR;

namespace Application.Problems.Commands;

public record AssignCoordinatorCommand : IRequest<Result<Problem, ProblemException>>
{
    public required Guid ProblemId { get; init; }
    public required Guid CoordinatorId { get; init; }
    public string? Priority { get; init; }
}

public class AssignCoordinatorCommandHandler(
    IProblemRepository problemRepository,
    IUserRepository userRepository,
    ISignalRService signalRService)
    : IRequestHandler<AssignCoordinatorCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(
        AssignCoordinatorCommand request,
        CancellationToken cancellationToken)
    {
        var problemId = new ProblemId(request.ProblemId);
        var coordinatorId = new UserId(request.CoordinatorId);
        
        var existingProblem = await problemRepository.GetById(problemId, cancellationToken);

        return await existingProblem.Match<Task<Result<Problem, ProblemException>>>(
            async problem =>
            {
                var coordinator = await userRepository.GetById(coordinatorId, cancellationToken);
                
                return await coordinator.Match<Task<Result<Problem, ProblemException>>>(
                    async _ =>
                    {
                        try
                        {
                            problem.AssignCoordinator(coordinatorId);
                            problem.UpdateStatus(ProblemStatus.InProgress);
                            if (!string.IsNullOrEmpty(request.Priority))
                            {
                                problem.UpdatePriority(Priority.From(request.Priority));
                            }
                            var result = await problemRepository.Update(problem, cancellationToken);

                            if (problem.CreatedById != null)
                            {
                                await signalRService.SendNotificationToUser(
                                    problem.CreatedById,
                                    NotificationDto.Create("status_change",
                                        $"Вашу проблему '{problem.Title}' взято в роботу координатором!",
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
                        new ProblemUnknownException(problemId, 
                            new Exception($"Coordinator with id {coordinatorId} not found")))
                );
            },
            () => Task.FromResult<Result<Problem, ProblemException>>(
                new ProblemNotFoundException(problemId))
        );
    }
}
