using API.DTOs.Users;
using Domain.Ratings;

namespace API.DTOs.Ratings;

public record RatingDto(
    Guid? Id,
    double Points,
    Guid ProblemId,
    UserDto? User,
    DateTime CreatedAt)
{
    public static RatingDto FromDomainModel(Rating rating)
        => new(
            rating.Id.Value,
            rating.Points,
            rating.ProblemId.Value,
            rating.User == null ? null : UserDto.FromDomainModel(rating.User),
            rating.CreatedAt
        );
}