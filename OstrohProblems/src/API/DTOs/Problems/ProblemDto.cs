using API.DTOs.Categories;
using API.DTOs.Comments;
using API.DTOs.Statuses;
using API.DTOs.Users;
using Domain.Problems;

namespace API.DTOs.Problems;

public record ProblemDto(
    Guid? Id,
    string Title,
    double Latitude,
    double Longitude,
    string Description,
    StatusDto? ProblemStatus,
    UserDto? User,
    List<CommentDto>? Comments,
    List<ProblemImageDto>? Images,
    List<CategoryDto>? Categories,
    DateTime CreatedAt,
    DateTime UpdatedAt)
{
    public static ProblemDto FromDomainModel(Problem problem)
        => new(
            problem.Id.Value,
            problem.Title,
            problem.Latitude,
            problem.Longitude,
            problem.Description,
            problem.ProblemStatus == null ? null : StatusDto.FromDomainModel(problem.ProblemStatus),
            problem.User == null ? null : UserDto.FromDomainModel(problem.User),
            problem.Comments.Count == 0 ? null : problem.Comments.Select(CommentDto.FromDomainModel).ToList(),
            problem.Images.Select(ProblemImageDto.FromDomainModel).ToList(),
            problem.Categories.Count == 0 ? null : problem.Categories.Select(CategoryDto.FromDomainModel).ToList(),
            problem.CreatedAt,
            problem.UpdatedAt
        );
}