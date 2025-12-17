namespace Domain.Problems;

public record CoordinatorImageId(Guid Value)
{
    public static CoordinatorImageId New() => new(Guid.NewGuid());
    public static CoordinatorImageId Empty => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}
