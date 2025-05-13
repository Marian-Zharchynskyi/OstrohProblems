using Domain.Identity.Users;
using Domain.Problems;

namespace Domain.Ratings;

public class Rating
{
    public RatingId Id { get; }
    public ProblemId ProblemId { get; private set; }
    public Problem? Problem { get; set; }
    public UserId UserId { get; private set; }
    public User User { get; set; }
    public double Points { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Rating(RatingId id, ProblemId problemId, UserId userId, double points,
        DateTime createdAt)
    {
        Id = id;
        ProblemId = problemId;
        UserId = userId;
        Points = points;
        CreatedAt = createdAt;
    }
    
    public static Rating New(RatingId id, ProblemId problemId, UserId userId, double points)
    {
        return new Rating(id, problemId, userId, points, DateTime.UtcNow);
    }
    
}