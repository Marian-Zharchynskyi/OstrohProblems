namespace Domain.Problems;

public class CoordinatorImage
{
    public CoordinatorImageId Id { get; }
    public Problem? Problem { get; private set; }
    public ProblemId ProblemId { get; }
    public string FilePath { get; private set; }

    private CoordinatorImage(CoordinatorImageId id, ProblemId problemId, string filePath)
    {
        Id = id;
        ProblemId = problemId;
        FilePath = filePath;
    }

    public static CoordinatorImage New(CoordinatorImageId id, ProblemId problemId, string filePath)
        => new CoordinatorImage(id, problemId, filePath);
}
