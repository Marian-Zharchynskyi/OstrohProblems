using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Ratings.Exceptions;
using Domain.Identity.Users;
using Domain.Problems;
using Domain.Ratings;
using MediatR;
using Microsoft.AspNetCore.Http;
using UserIdNotFoundException = Application.Ratings.Exceptions.UserIdNotFoundException;

namespace Application.Ratings.Commands;

public class CreateRatingCommand : IRequest<Result<Rating, RatingException>>
{
    public required double Points { get; init; }
    public required Guid ProblemId { get; init; }
}

public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, Result<Rating, RatingException>>
{
    private readonly IRatingRepository _ratingRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateRatingCommandHandler(
        IRatingRepository ratingRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _ratingRepository = ratingRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<Rating, RatingException>> Handle(
        CreateRatingCommand request,
        CancellationToken cancellationToken)
    {
        var ratingId = RatingId.New();
        var problemId = new ProblemId(request.ProblemId);

        try
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?
                .Claims.FirstOrDefault(c => c.Type == "id");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userIdGuid))
            {
                return new UserIdNotFoundException(ratingId);
            }

            var userId = new UserId(userIdGuid);

            // Check if user already has a rating for this problem
            var existingRating = await _ratingRepository.GetByUserAndProblem(userId, problemId, cancellationToken);

            return await existingRating.Match(
                async rating =>
                {
                    // Update existing rating
                    rating.UpdatePoints(request.Points);
                    return await _ratingRepository.Update(rating, cancellationToken);
                },
                async () =>
                {
                    // Create new rating
                    var rating = Rating.New(ratingId, problemId, userId, request.Points);
                    return await _ratingRepository.Add(rating, cancellationToken);
                });
        }
        catch (Exception exception)
        {
            return new RatingUnknownException(ratingId, exception);
        }
    }
}