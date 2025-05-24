using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Statuses.Exceptions;
using Domain.Statuses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Application.Statuses.Commands;

public record DeleteStatusCommand : IRequest<Result<Status, StatusException>>
{
    public required Guid ProblemStatusId { get; init; }
}

public class DeleteProblemStatusCommandHandler(
    IStatusRepository statusRepository)
    : IRequestHandler<DeleteStatusCommand, Result<Status, StatusException>>
{
    public async Task<Result<Status, StatusException>> Handle(
        DeleteStatusCommand request,
        CancellationToken cancellationToken)
    {
        var problemStatusId = new StatusId(request.ProblemStatusId);
        var existingProblemStatus = await statusRepository.GetById(problemStatusId, cancellationToken);

        return await existingProblemStatus.Match<Task<Result<Status, StatusException>>>(
            async problemStatus => await DeleteEntity(problemStatus, cancellationToken),
            () => Task.FromResult<Result<Status, StatusException>>
                (new StatusNotFoundException(problemStatusId)));
    }

    private async Task<Result<Status, StatusException>> DeleteEntity(
        Status status,
        CancellationToken cancellationToken)
    {
        try
        {
            return await statusRepository.Delete(status, cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503")
        {
            return new StatusHasRelatedProductsException(status.Id);
        }
        catch (Exception exception)
        {
            return new StatusUnknownException(status.Id, exception);
        }
    }
}