using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Problems.Exceptions;
using Domain.Problems;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Application.Problems.Commands;

public record DeleteProblemCommand : IRequest<Result<Problem, ProblemException>>
{
    public required Guid ProblemId { get; init; }
}

public class DeleteProblemCommandHandler(
    IProblemRepository problemRepository)
    : IRequestHandler<DeleteProblemCommand, Result<Problem, ProblemException>>
{
    public async Task<Result<Problem, ProblemException>> Handle(
        DeleteProblemCommand request,
        CancellationToken cancellationToken)
    {
        var problemId = new ProblemId(request.ProblemId);
        var existingProblem = await problemRepository.GetById(problemId, cancellationToken);

        return await existingProblem.Match<Task<Result<Problem, ProblemException>>>(
            async problem => await DeleteEntity(problem, cancellationToken),
            () => Task.FromResult<Result<Problem, ProblemException>>
                (new ProblemNotFoundException(problemId)));
    }

    private async Task<Result<Problem, ProblemException>> DeleteEntity(
        Problem problem,
        CancellationToken cancellationToken)
    {
        try
        {
            return await problemRepository.Delete(problem, cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503")
        {
            return new ProblemHasRelatedEntitiesException(problem.Id);
        }
        catch (Exception exception)
        {
            return new ProblemUnknownException(problem.Id, exception);
        }
    }
}