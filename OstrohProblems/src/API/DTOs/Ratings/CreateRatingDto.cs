using Domain.Ratings;

namespace API.DTOs.Ratings;

public record CreateRatingDto(double Points, Guid ProblemId)
{
    public static CreateRatingDto FromDomainModel(Rating rating)
        => new(
            rating.Points,
            rating.ProblemId.Value
        );
}