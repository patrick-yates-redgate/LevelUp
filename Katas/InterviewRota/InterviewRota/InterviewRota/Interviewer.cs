namespace InterviewRota;

public class Interviewer
{
    public string Name { get; }
    public List<string?> Languages { get; }
    public int ActualEffort { get; set; } = 0;
    public int PendingEffort { get; set; } = 0;
    public bool IsAvailable { get; set; } = true;

    public Interviewer(string name, List<string?>? languages = null)
    {
        Name = name;
        Languages = languages ?? new List<string?>();
    }
}