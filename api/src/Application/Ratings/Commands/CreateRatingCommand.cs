using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Ratings.Exceptions;
using Application.Services.SignalRService;
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
    private readonly ISignalRService _signalRService;
    private readonly IProblemQueries _problemQueries;

    public CreateRatingCommandHandler(
        IRatingRepository ratingRepository,
        IHttpContextAccessor httpContextAccessor,
        ISignalRService signalRService,
        IProblemQueries problemQueries)
    {
        _ratingRepository = ratingRepository;
        _httpContextAccessor = httpContextAccessor;
        _signalRService = signalRService;
        _problemQueries = problemQueries;
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
                    var result = await _ratingRepository.Update(rating, cancellationToken);
                    await SendRatingNotificationToOwner(problemId, userIdGuid, cancellationToken);
                    return result;
                },
                async () =>
                {
                    // Create new rating
                    var rating = Rating.New(ratingId, problemId, userId, request.Points);
                    var result = await _ratingRepository.Add(rating, cancellationToken);
                    await SendRatingNotificationToOwner(problemId, userIdGuid, cancellationToken);
                    return result;
                });
        }
        catch (Exception exception)
        {
            return new RatingUnknownException(ratingId, exception);
        }
    }

    private async Task SendRatingNotificationToOwner(ProblemId problemId, Guid raterUserId, CancellationToken cancellationToken)
    {
        var problemOption = await _problemQueries.GetById(problemId, cancellationToken);
        if (problemOption.HasValue)
        {
            var problem = problemOption.ValueOr(default(Problem)!);
            if (problem is not null && problem.CreatedById is not null && problem.CreatedById.Value != raterUserId)
            {
                var notification = NotificationDto.Create(
                    "rating",
                    "Хтось оцінив вашу проблему",
                    problem.Id.Value.ToString(),
                    problem.Title);

                await _signalRService.SendNotificationToUser(
                    problem.CreatedById,
                    notification,
                    cancellationToken);
            }
        }
    }
}