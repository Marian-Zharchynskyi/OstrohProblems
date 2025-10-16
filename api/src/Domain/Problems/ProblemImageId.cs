namespace Domain.Problems;

public record ProblemImageId(Guid Value)
{
    public static ProblemImageId Empty => new(Guid.Empty);
    public static ProblemImageId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}