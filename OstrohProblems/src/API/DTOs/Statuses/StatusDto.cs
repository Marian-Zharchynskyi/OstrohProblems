using Domain.Statuses;

namespace API.DTOs.Statuses;

public record StatusDto(Guid? Id, string Name)
{
    public static StatusDto FromDomainModel(Status status)
        => new(
            status.Id.Value,
            status.Name
        );
}