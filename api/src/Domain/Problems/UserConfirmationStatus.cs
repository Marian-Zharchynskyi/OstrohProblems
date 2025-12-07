using Domain.Common;

namespace Domain.Problems;

public static class UserConfirmationStatusConstants
{
    public const string Pending = "Очікує підтвердження";
    public const string Confirmed = "Підтверджено";
    public const string Rejected = "Відхилено користувачем";

    public static readonly IReadOnlyList<string> All = new[]
    {
        Pending,
        Confirmed,
        Rejected
    };
}

public class UserConfirmationStatus : ValueObject
{
    public string Value { get; }

    private UserConfirmationStatus(string value)
    {
        Value = value;
    }

    public static UserConfirmationStatus From(string value)
    {
        if (!UserConfirmationStatusConstants.All.Contains(value))
        {
            throw new ArgumentException($"Invalid user confirmation status: {value}");
        }

        return new UserConfirmationStatus(value);
    }

    public static UserConfirmationStatus Pending => new(UserConfirmationStatusConstants.Pending);
    public static UserConfirmationStatus Confirmed => new(UserConfirmationStatusConstants.Confirmed);
    public static UserConfirmationStatus Rejected => new(UserConfirmationStatusConstants.Rejected);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
