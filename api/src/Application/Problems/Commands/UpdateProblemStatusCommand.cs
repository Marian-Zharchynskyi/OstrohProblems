using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Application.Services.SignalRService;
using Domain.Problems;
using MediatR;

namespace Application.Problems.Commands;

public record UpdateProblemStatusCommand : IRequest<Result<Problem, ProblemException>>
{
    public required Guid ProblemId { get; init; }
    public required string Status { get; init; }
}

public class UpdateProblemStatusCommandHandler(
    IProblemRepository problemRepository,
    ISignalRService signalRService)
    : IRequestHandler<UpdateProblemStatusCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(
        UpdateProblemStatusCommand request,
        CancellationToken cancellationToken)
    {
        var problemId = new ProblemId(request.ProblemId);
        var existingProblem = await problemRepository.GetById(problemId, cancellationToken);

        return await existingProblem.Match<Task<Result<Problem, ProblemException>>>(
            async problem =>
            {
                try
                {
                    var newStatus = ProblemStatus.From(request.Status);
                    var oldStatus = problem.Status;
                    
                    problem.UpdateStatus(newStatus);
                    var result = await problemRepository.Update(problem, cancellationToken);

                    if (oldStatus != newStatus && problem.CreatedById != null)
                    {
                        await signalRService.SendNotificationToUser(
                            problem.CreatedById,
                            NotificationDto.Create("status_change",
                                $"Статус вашої проблеми змінено на: {newStatus.Value}",
                                problem.Id.Value.ToString(),
                                problem.Title),
                            cancellationToken);
                    }

                    return result;
                }
                catch (UnsupportedProblemStatusException ex)
                {
                    return new ProblemUnknownException(problem.Id, ex);
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
