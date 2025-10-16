namespace Domain.Ratings;

public record RatingId(Guid Value)
{
    public static RatingId New() => new(Guid.NewGuid());
    public static RatingId Empty => new(Guid.Empty);
    public override string ToString() => Value.ToString();
}