namespace API.DTOs.Problems;

public record UpdateProblemTitleAndCategoriesDto(string Title, List<string>? CategoryNames);
