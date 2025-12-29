using Domain.Problems;

namespace API.DTOs.Problems;

public record ProblemSummaryDto(
    Guid? Id,
    string Title,
    double Latitude,
    double Longitude,
    string Description,
    string Status,
    string Priority,
    List<string>? Categories,
    DateTime CreatedAt,
    DateTime UpdatedAt)
{
    public static ProblemSummaryDto FromDomainModel(Problem problem)
        => new(
            problem.Id.Value,
            problem.Title,
            problem.Latitude,
            problem.Longitude,
            problem.Description,
            problem.Status.Value,
            problem.Priority.Value,
            problem.Categories.Count == 0 ? null : problem.Categories.Select(c => c.Value).ToList(),
            problem.CreatedAt,
            problem.UpdatedAt
        );
}
