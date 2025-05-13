using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Ratings.Exceptions;
using Domain.Comments;
using Domain.Ratings;
using MediatR;

namespace Application.Ratings.Commands;

public record UpdateRatingCommand : IRequest<Result<Rating, RatingException>>
{
    public required Guid RatingId { get; init; }
    public required double Points { get; init; }
}

public class UpdateRatingCommandHandler(
    IRatingRepository ratingRepository)
    : IRequestHandler<UpdateRatingCommand, Result<Rating, RatingException>>
{
    public async Task<Result<Rating, RatingException>> Handle(
        UpdateRatingCommand request,
        CancellationToken cancellationToken)
    {
        var ratingId = new RatingId(request.RatingId);
        var existingRating = await ratingRepository.GetById(ratingId, cancellationToken);

        return await existingRating.Match<Task<Result<Rating, RatingException>>>(
            async rating => await UpdateRating(rating, request.Points, cancellationToken),
            () => Task.FromResult<Result<Rating, RatingException>>(
                new RatingNotFoundException(ratingId))
        );
    }

    private async Task<Result<Rating, RatingException>> UpdateRating(
        Rating rating,
        double points,
        CancellationToken cancellationToken)
    {
        try
        {
            rating.UpdatePoints(points);
            return await ratingRepository.Update(rating, cancellationToken);
        }
        catch (Exception exception)
        {
            return new RatingUnknownException(rating.Id, exception);
        }
    }
}
