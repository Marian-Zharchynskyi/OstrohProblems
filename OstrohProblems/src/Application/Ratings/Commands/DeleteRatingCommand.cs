using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Ratings.Exceptions;
using Domain.Ratings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Application.Ratings.Commands;

public record DeleteRatingCommand : IRequest<Result<Rating, RatingException>>
{
    public required Guid RatingId { get; init; }
}

public class DeleteRatingCommandHandler(
    IRatingRepository ratingRepository)
    : IRequestHandler<DeleteRatingCommand, Result<Rating, RatingException>>
{
    public async Task<Result<Rating, RatingException>> Handle(
        DeleteRatingCommand request,
        CancellationToken cancellationToken)
    {
        var ratingId = new RatingId(request.RatingId);
        var existingRating = await ratingRepository.GetById(ratingId, cancellationToken);

        return await existingRating.Match<Task<Result<Rating, RatingException>>>(
            async rating => await DeleteEntity(rating, cancellationToken),
            () => Task.FromResult<Result<Rating, RatingException>>(
                new RatingNotFoundException(ratingId))
        );
    }

    private async Task<Result<Rating, RatingException>> DeleteEntity(
        Rating rating,
        CancellationToken cancellationToken)
    {
        try
        {
            return await ratingRepository.Delete(rating, cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503")
        {
            return new RatingHasRelatedEntitiesException(rating.Id);
        }
        catch (Exception exception)
        {
            return new RatingUnknownException(rating.Id, exception);
        }
    }
}