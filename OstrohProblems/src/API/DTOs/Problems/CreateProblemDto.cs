using Domain.Problems;

namespace API.DTOs.Problems;

public record CreateProblemDto(
    string Title,
    double Latitude,
    double Longitude,
    string Description,
    Guid ProblemStatusId,
    List<Guid> ProblemCategoryIds
)
{
    public static CreateProblemDto FromDomainModel(Problem problem)
        => new(
            problem.Title,
            problem.Latitude,
            problem.Longitude,
            problem.Description,
            problem.StatusId.Value,
            problem.Categories.Select(c => c.Id.Value).ToList()
        );
}