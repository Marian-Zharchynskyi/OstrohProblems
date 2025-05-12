using API.DTOs.Users;
using Domain.Problems;

namespace API.DTOs.Problems;

public record CreateProblemDto(
    string Title,
    double Latitude,
    double Longitude,
    string Description,
    Guid ProblemStatusId,
    UserDto? User,
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
            problem.User == null ? null : UserDto.FromDomainModel(problem.User),
            problem.Categories.Select(c => c.Id.Value).ToList()
        );
}