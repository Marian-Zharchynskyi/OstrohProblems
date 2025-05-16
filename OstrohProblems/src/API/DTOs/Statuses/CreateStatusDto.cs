using Domain.Statuses;

namespace API.DTOs.Statuses;

public record CreateStatusDto(string Name)
{
    public static CreateStatusDto FromDomainModel(Status status)
        => new(
            status.Name
        );
}