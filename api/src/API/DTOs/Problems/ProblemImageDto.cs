using Domain.Problems;

namespace API.DTOs.Problems;

public record ProblemImageDto(Guid? Id, string Url)
{
    public static ProblemImageDto FromDomainModel(ProblemImage problemImage, Func<string, string> getImageUrl)
        => new(problemImage.Id.Value, getImageUrl(problemImage.FilePath));
}