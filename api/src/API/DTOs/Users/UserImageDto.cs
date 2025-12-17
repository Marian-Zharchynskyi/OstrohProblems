using Domain.Identity.Users;

namespace API.DTOs.Users;

public record UserImageDto(Guid? Id, string Url)
{
    public static UserImageDto FromDomainModel(UserImage userImage, Func<string, string> getImageUrl)
        => new(userImage.Id.Value, getImageUrl(userImage.FilePath));
}