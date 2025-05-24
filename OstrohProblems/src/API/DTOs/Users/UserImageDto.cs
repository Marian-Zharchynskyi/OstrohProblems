using Domain.Identity.Users;

namespace API.DTOs.Users;

public record UserImageDto(Guid? Id, string FilePath)
{
    public static UserImageDto FromDomainModel(UserImage userImage)
    => new(userImage.Id.Value, userImage.FilePath);
}