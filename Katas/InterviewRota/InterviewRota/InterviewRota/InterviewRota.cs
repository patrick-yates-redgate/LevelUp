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
    }

    public void ReportUnavailable(string interviewerName)
    {
        var interviewer = _interviewers.First(i => i.Name == interviewerName);
        interviewer.IsAvailable = false;
    }
    
    public string GetNextInterviewer(int effort = 1, string? language = null)
    {
        IEnumerable<Interviewer> orderedPairs = _interviewers
            .OrderBy(interviewer => interviewer.ActualEffort + interviewer.PendingEffort);

        if (language != null)
        {
            orderedPairs = orderedPairs.Where(x => x.Languages.Contains(language));
        }

        foreach (var interviewer in orderedPairs)
        {
            if (interviewer.IsAvailable)
            {
                interviewer.PendingEffort += effort;
        
                return interviewer.Name;
            }
            
            interviewer.IsAvailable = true;
        }
        
        throw new Exception("No interviewers available");
    }
}