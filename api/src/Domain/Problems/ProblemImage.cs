namespace Domain.Problems;

public class ProblemImage
{
    public ProblemImageId Id { get; }
    public Problem? Problem { get; private set; } 
    public ProblemId ProblemId { get; }
    public string FilePath { get; private set; }

    private ProblemImage(ProblemImageId id, ProblemId problemId, string filePath)
    {
        Id = id; 
        ProblemId = problemId;
        FilePath = filePath;
    }

    public static ProblemImage New(ProblemImageId id, ProblemId problemId, string filePath) 
        => new ProblemImage(id, problemId, filePath);
}