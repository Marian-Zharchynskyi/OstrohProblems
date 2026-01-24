namespace API.DTOs.Problems;

public record UserProblemsFilterDto(
    string? SearchTerm,
    string? Status,
    string? Category,
    string? Priority,
    string? SortBy,
    bool SortDescending = true,
    string? DateFilter = null);
