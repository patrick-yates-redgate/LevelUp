namespace InterviewRota;

public class InterviewRota
{
    private List<Interviewer> _interviewers = new();

    public InterviewRota(IEnumerable<string> interviewers)
    {
        foreach (var interviewer in interviewers)
        {
            _interviewers.Add(new Interviewer(interviewer));
        }
    }

    public void RecordInterview(string interviewerName, int effort = 1, string? language = null)
    {
        var interviewer = _interviewers.First(i => i.Name == interviewerName);
        interviewer.ActualEffort += effort;

        if (language != null && !interviewer.Languages.Contains(language))
        {
            interviewer.Languages.Add(language);
        }
        
        _interviewers.ForEach(x => x.IsAvailable = true);
    }

    public void ReportUnavailable(string interviewerName)
    {
        var interviewer = _interviewers.First(i => i.Name == interviewerName);
        interviewer.IsAvailable = false;
    }

    public string GetNextInterviewer(int effort = 1, string? language = null)
    {
        IEnumerable<Interviewer> orderedPairs = _interviewers
            .Where(interviewer => interviewer.IsAvailable)
            .OrderBy(interviewer => interviewer.ActualEffort + interviewer.PendingEffort);

        if (language != null)
        {
            orderedPairs = orderedPairs.Where(x => x.Languages.Contains(language));
        }

        var interviewer = orderedPairs.FirstOrDefault();
        interviewer.PendingEffort += effort;
        
        return interviewer.Name;
    }
}