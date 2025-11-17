using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Domain.Identity.Users;
using Domain.Problems;
using MediatR;

namespace Application.Problems.Commands;

public record AssignCoordinatorCommand : IRequest<Result<Problem, ProblemException>>
{
    public required Guid ProblemId { get; init; }
    public required Guid CoordinatorId { get; init; }
}

public class AssignCoordinatorCommandHandler(
    IProblemRepository problemRepository,
    IUserRepository userRepository)
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
                            return await problemRepository.Update(problem, cancellationToken);
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
