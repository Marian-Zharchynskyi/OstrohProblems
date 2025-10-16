using Domain.Categories;
using Domain.Comments;
using Domain.Identity.Users;
using Domain.Ratings;
using Domain.Statuses;

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
    public StatusId StatusId { get; private set; }
    public Status? ProblemStatus { get; set; }
    public UserId UserId { get; private set; }
    public User? User { get; set; }
    public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
    public ICollection<Category> Categories { get; private set; } = new List<Category>();
    public ICollection<Rating> Ratings { get; private set; } = new List<Rating>();
    public List<ProblemImage> Images { get; private set; } = [];

    private Problem(ProblemId id, string title, double latitude, double longitude, string description,
        StatusId statusId, DateTime createdAt, DateTime updatedAt, UserId userId)
    {
        Id = id;
        Title = title;
        Latitude = latitude;
        Longitude = longitude;
        Description = description;
        StatusId = statusId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        UserId = userId;
    }

    public static Problem New(ProblemId id, string title, double latitude, double longitude, string description,
        StatusId statusId, UserId userId)
    {
        return new Problem(id, title, latitude, longitude, description, statusId, DateTime.UtcNow,
            DateTime.UtcNow, userId);
    }

    public void UpdateProblem(string title, double latitude, double longitude, string description, StatusId statusId)
    {
        Title = title;
        Latitude = latitude;
        Longitude = longitude;
        Description = description;
        StatusId = statusId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddCategory(Category category)
    {
        if (Categories.Any(c => c.Id == category.Id))
            return;

        Categories.Add(category);
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
}