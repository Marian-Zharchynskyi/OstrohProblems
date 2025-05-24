using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Statuses.Exceptions;
using Domain.Statuses;
using MediatR;

namespace Application.Statuses.Commands;

public record UpdateStatusCommand : IRequest<Result<Status, StatusException>>
{
    public required Guid ProblemStatusId { get; init; }
    public required string Name { get; init; }
}

public class UpdateProblemStatusCommandHandler(
    IStatusRepository statusRepository)
    : IRequestHandler<UpdateStatusCommand, Result<Status, StatusException>>
{
    public async Task<Result<Status, StatusException>> Handle(
        UpdateStatusCommand request,
        CancellationToken cancellationToken)
    {
        var problemStatusId = new StatusId(request.ProblemStatusId);
        var existingProblemStatus = await statusRepository.GetById(problemStatusId, cancellationToken);

        return await existingProblemStatus.Match<Task<Result<Status, StatusException>>>(
            async problemStatus => await UpdateProblemStatus(problemStatus, request.Name, cancellationToken),
            () => Task.FromResult<Result<Status, StatusException>>(
                new StatusNotFoundException(problemStatusId))
        );
    }

    private async Task<Result<Status, StatusException>> UpdateProblemStatus(
        Status status,
        string name,
        CancellationToken cancellationToken)
    {
        try
        {
            status.UpdateName(name);
            return await statusRepository.Update(status, cancellationToken);
        }
        catch (Exception exception)
        {
            return new StatusUnknownException(status.Id, exception);
        }
    }
}