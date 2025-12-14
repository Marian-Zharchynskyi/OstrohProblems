using Domain.Comments;
using Domain.Identity.Users;
using Domain.Ratings;

namespace Domain.Problems;

public class Problem
{
    public ProblemId Id { get; }
    public string Title { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public ProblemStatus Status { get; private set; }
    public UserId CreatedById { get; private set; }
    public User? CreatedBy { get; set; }
    public UserId? CoordinatorId { get; private set; }
    public User? Coordinator { get; set; }
    public string? RejectionReason { get; private set; }
    public string? CoordinatorComment { get; private set; }
    public string? CurrentState { get; private set; }
    public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
    public List<Category> Categories { get; private set; } = new();
    public ICollection<Rating> Ratings { get; private set; } = new List<Rating>();
    public List<ProblemImage> Images { get; private set; } = new();
    public List<CoordinatorImage> CoordinatorImages { get; private set; } = new();

    private Problem(ProblemId id, string title, double latitude, double longitude, string description,
        ProblemStatus status, DateTime createdAt, DateTime updatedAt, UserId createdById)
    {
        Id = id;
        Title = title;
        Latitude = latitude;
        Longitude = longitude;
        Description = description;
        Status = status;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        CreatedById = createdById;
    }

    public static Problem New(ProblemId id, string title, double latitude, double longitude, string description,
        UserId createdById)
    {
        return new Problem(id, title, latitude, longitude, description, ProblemStatus.New, DateTime.UtcNow,
            DateTime.UtcNow, createdById);
    }

    public void UpdateProblem(string title, double latitude, double longitude, string description)
    {
        Title = title;
        Latitude = latitude;
        Longitude = longitude;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(ProblemStatus status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateCurrentState(string currentState)
    {
        CurrentState = currentState;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddCategory(string categoryName)
    {
        var category = Category.From(categoryName);
        if (Categories.Any(c => c.Value == category.Value))
            return;

        Categories.Add(category);
    }

    public void SetCategories(IEnumerable<string> categoryNames)
    {
        Categories.Clear();
        foreach (var name in categoryNames)
        {
            var category = Category.From(name);
            Categories.Add(category);
        }
        UpdatedAt = DateTime.UtcNow;
    }

    public void UploadProblemImages(List<ProblemImage> images)
        => Images.AddRange(images);

    public void RemoveImage(ProblemImageId productImageId)
    {
        var image = Images.FirstOrDefault(x => x.Id == productImageId);
        if (image != null)
        {
            Images.Remove(image);
        }
    }

    public void AssignCoordinator(UserId coordinatorId)
    {
        CoordinatorId = coordinatorId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ClearCoordinator()
    {
        CoordinatorId = null;
        Coordinator = null;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reject(string reason)
    {
        RejectionReason = reason;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ClearRejection()
    {
        RejectionReason = null;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetCoordinatorComment(string comment)
    {
        CoordinatorComment = comment;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UploadCoordinatorImages(List<CoordinatorImage> images)
        => CoordinatorImages.AddRange(images);

    public void RemoveCoordinatorImage(CoordinatorImageId coordinatorImageId)
    {
        var image = CoordinatorImages.FirstOrDefault(x => x.Id == coordinatorImageId);
        if (image != null)
        {
            CoordinatorImages.Remove(image);
        }
    }
}