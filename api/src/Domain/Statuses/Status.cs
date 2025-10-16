using Domain.Problems;

namespace Domain.Statuses;

public class Status
{
    public StatusId Id { get; }
    public string Name { get; private set; }
    public ICollection<Problem> Problems { get; private set; } = new List<Problem>();

    private Status(StatusId id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Status New(StatusId id, string name)
    {
        return new Status(id, name);
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}