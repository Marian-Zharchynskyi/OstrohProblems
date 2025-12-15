namespace API.DTOs.Problems;

public record RejectProblemDto(Guid CoordinatorId, string RejectionReason);
