using Domain.Identity.Users;
using Domain.Problems;

namespace Domain.Comments;

public class Comment
{
    public CommentId Id { get; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public ProblemId ProblemId { get; private set; }
    public Problem? Problem { get; set; }
    public UserId UserId { get; private set; }
    public User? User { get; set; }

    private Comment(CommentId id, string content, DateTime createdAt, ProblemId problemId, UserId userId)
    {
        Id = id;
        Content = content;
        CreatedAt = createdAt;
        ProblemId = problemId;
        UserId = userId;
    }

    public static Comment New(CommentId id, string content, ProblemId problemId, UserId userId)
    {
        return new Comment(id, content, DateTime.UtcNow, problemId, userId);
    }

    public void UpdateContent(string content)
    {
        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }
}