using Domain.Problems;

namespace API.DTOs.Problems;

public record ProblemImageDto(Guid? Id, string FilePath)
{
    public static ProblemImageDto FromDomainModel(ProblemImage userImage)
        => new(userImage.Id.Value, userImage.FilePath);
}