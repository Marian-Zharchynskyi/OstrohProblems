using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Application.Services.SignalRService;
using Domain.Identity.Users;
using Domain.Problems;
using MediatR;

namespace Application.Problems.Commands;

public record ValidateProblemCommand : IRequest<Result<Problem, ProblemException>>
{
    public required Guid ProblemId { get; init; }
    public required Guid CoordinatorId { get; init; }
}

public class ValidateProblemCommandHandler(
    IProblemRepository problemRepository,
    IUserRepository userRepository,
    ISignalRService signalRService)
    : IRequestHandler<ValidateProblemCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(
        ValidateProblemCommand request,
        CancellationToken cancellationToken)
    {
        var problemId = new ProblemId(request.ProblemId);
        var coordinatorId = new UserId(request.CoordinatorId);
        
        var existingProblem = await problemRepository.GetById(problemId, cancellationToken);

        return await existingProblem.Match<Task<Result<Problem, ProblemException>>>(
            async problem =>
            {
                var coordinator = await userRepository.GetById(coordinatorId, cancellationToken);
                
                return await coordinator.Match(
                    async _ =>
                    {
                        try
                        {
                            problem.AssignCoordinator(coordinatorId);
                            problem.UpdateStatus(ProblemStatus.Validated);
                            var result = await problemRepository.Update(problem, cancellationToken);

                            if (problem.CreatedById != null)
                            {
                                await signalRService.SendNotificationToUser(
                                    problem.CreatedById,
                                    NotificationDto.Create("status_change",
                                        $"Вашу проблему '{problem.Title}' прийнято координатором!",
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
                        new ProblemUnknownException(problemId, 
                            new Exception($"Coordinator with id {coordinatorId} not found")))
                );
            },
            () => Task.FromResult<Result<Problem, ProblemException>>(
                new ProblemNotFoundException(problemId))
        );
    }
}
