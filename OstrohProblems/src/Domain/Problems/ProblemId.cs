namespace Domain.Problems;

public record ProblemId(Guid Value)
{
    public static ProblemId New() => new(Guid.NewGuid());
    public static ProblemId Empty => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}