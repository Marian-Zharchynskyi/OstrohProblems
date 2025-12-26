namespace API.DTOs.Problems;

public record AssignCoordinatorDto(
    Guid CoordinatorId,
    string? Priority = null);
