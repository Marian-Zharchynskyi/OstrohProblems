using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Statuses.Exceptions;
using Domain.Statuses;
using MediatR;

namespace Application.Statuses.Commands;

public record CreateStatusCommand : IRequest<Result<Status, StatusException>>
{
    public required string Name { get; init; }
}

public class CreateProblemStatusCommandHandler(
    IStatusRepository statusRepository)
    : IRequestHandler<CreateStatusCommand, Result<Status, StatusException>>
{
    public async Task<Result<Status, StatusException>> Handle(
        CreateStatusCommand request,
        CancellationToken cancellationToken)
    {
        var existingProblemStatus = await statusRepository.
            SearchByName(request.Name, cancellationToken);

        return await existingProblemStatus.Match(
            c => Task.FromResult<Result<Status, StatusException>>(
                new StatusAlreadyExistsException(c.Id)),
            async () => await CreateEntity(request.Name, cancellationToken));
    }

    private async Task<Result<Status, StatusException>> CreateEntity(
        string name,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = Status.New(StatusId.New(), name);

            return await statusRepository.Add(entity, cancellationToken);
        }
        catch (Exception exception)
        {
            return new StatusUnknownException(StatusId.Empty, exception);
        }
    }
}