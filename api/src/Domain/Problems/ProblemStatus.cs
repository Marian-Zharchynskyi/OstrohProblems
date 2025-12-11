using Domain.Common;

namespace Domain.Problems;

public class ProblemStatusConstants
{
    public const string New = "Нова";
    public const string InProgress = "В роботі";
    public const string Completed = "Виконано";
    public const string Rejected = "Відхилено";
}

public class ProblemStatus : ValueObject
{
    public string Value { get; }

    private ProblemStatus(string value)
    {
        Value = value;
    }

    public static ProblemStatus From(string value)
    {
        var status = new ProblemStatus(value);

        if (!SupportedStatuses.Contains(status))
        {
            throw new UnsupportedProblemStatusException(value);
        }

        return status;
    }

    public static ProblemStatus New => new(ProblemStatusConstants.New);
    public static ProblemStatus InProgress => new(ProblemStatusConstants.InProgress);
    public static ProblemStatus Completed => new(ProblemStatusConstants.Completed);
    public static ProblemStatus Rejected => new(ProblemStatusConstants.Rejected);

    public static implicit operator string(ProblemStatus status)
    {
        return status.ToString();
    }

    public static explicit operator ProblemStatus(string value)
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

    private static IEnumerable<ProblemStatus> SupportedStatuses
    {
        get
        {
            yield return New;
            yield return InProgress;
            yield return Completed;
            yield return Rejected;
        }
    }
}

public class UnsupportedProblemStatusException : Exception
{
    public UnsupportedProblemStatusException(string status)
        : base($"Problem status '{status}' is not supported.") { }
}
