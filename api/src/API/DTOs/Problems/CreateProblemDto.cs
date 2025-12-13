using Domain.Problems;

namespace API.DTOs.Problems;

public record CreateProblemDto(
    Guid? Id,
    string Title,
    double Latitude,
    double Longitude,
    string Description,
    List<Guid> ProblemCategoryIds
)
{
    public static CreateProblemDto FromDomainModel(Problem problem)
        => new(
            problem.Id.Value,
            problem.Title,
            problem.Latitude,
            problem.Longitude,
            problem.Description,
            problem.Categories.Select(c => c.Id.Value).ToList()
        );
}