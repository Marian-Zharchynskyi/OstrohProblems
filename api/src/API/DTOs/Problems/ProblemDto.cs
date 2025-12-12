using API.DTOs.Categories;
using API.DTOs.Comments;
using API.DTOs.Users;
using Domain.Problems;

namespace API.DTOs.Problems;

public record ProblemDto(
    Guid? Id,
    string Title,
    double Latitude,
    double Longitude,
    string Description,
    string Status,
    UserDto? CreatedBy,
    UserDto? Coordinator,
    string? RejectionReason,
    string? CoordinatorComment,
    string? CurrentState,
    List<CommentDto>? Comments,
    List<ProblemImageDto>? Images,
    List<CoordinatorImageDto>? CoordinatorImages,
    List<CategoryDto>? Categories,
    DateTime CreatedAt,
    DateTime UpdatedAt)
{
    public static ProblemDto FromDomainModel(Problem problem, Func<string, string>? getImageUrl = null)
        => new(
            problem.Id.Value,
            problem.Title,
            problem.Latitude,
            problem.Longitude,
            problem.Description,
            problem.Status.Value,
            problem.CreatedBy == null ? null : UserDto.FromDomainModel(problem.CreatedBy, getImageUrl),
            problem.Coordinator == null ? null : UserDto.FromDomainModel(problem.Coordinator, getImageUrl),
            problem.RejectionReason,
            problem.CoordinatorComment,
            problem.CurrentState,
            problem.Comments.Count == 0 ? null : problem.Comments.Select(CommentDto.FromDomainModel).ToList(),
            getImageUrl != null 
                ? problem.Images.Select(i => ProblemImageDto.FromDomainModel(i, getImageUrl)).ToList() 
                : new List<ProblemImageDto>(),
            getImageUrl != null 
                ? problem.CoordinatorImages.Select(i => CoordinatorImageDto.FromDomainModel(i, getImageUrl)).ToList() 
                : new List<CoordinatorImageDto>(),
            problem.Categories.Count == 0 ? null : problem.Categories.Select(CategoryDto.FromDomainModel).ToList(),
            problem.CreatedAt,
            problem.UpdatedAt
        );
}