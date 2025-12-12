using Domain.Problems;

namespace API.DTOs.Problems;

public record CoordinatorImageDto(Guid? Id, string Url)
{
    public static CoordinatorImageDto FromDomainModel(CoordinatorImage coordinatorImage, Func<string, string> getImageUrl)
        => new(coordinatorImage.Id.Value, getImageUrl(coordinatorImage.FilePath));
}
