using Domain.Common;

namespace Domain.Problems;

public class PriorityConstants
{
    public const string Low = "Низький";
    public const string Medium = "Середній";
    public const string High = "Високий";
    public const string Critical = "Критичний";
}

public class Priority : ValueObject
{
    public string Value { get; }

    private Priority(string value)
    {
        Value = value;
    }

    public static Priority From(string value)
    {
        var priority = new Priority(value);

        if (!SupportedPriorities.Contains(priority))
        {
            throw new UnsupportedPriorityException(value);
        }

        return priority;
    }

    public static Priority Low => new(PriorityConstants.Low);
    public static Priority Medium => new(PriorityConstants.Medium);
    public static Priority High => new(PriorityConstants.High);
    public static Priority Critical => new(PriorityConstants.Critical);

    public static implicit operator string(Priority priority)
    {
        return priority.ToString();
    }

    public static explicit operator Priority(string value)
    {
        return From(value);
    }

    public override string ToString()
    {
        return Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static IEnumerable<Priority> SupportedPriorities
    {
        get
        {
            yield return Low;
            yield return Medium;
            yield return High;
            yield return Critical;
        }
    }

    public static IEnumerable<string> AllPriorityNames => SupportedPriorities.Select(p => p.Value);
}

public class UnsupportedPriorityException : Exception
{
    public UnsupportedPriorityException(string priority)
        : base($"Priority '{priority}' is not supported.") { }
}
