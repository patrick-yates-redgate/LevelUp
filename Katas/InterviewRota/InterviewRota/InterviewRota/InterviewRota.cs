namespace InterviewRota;

public class InterviewRota
{
    private readonly List<Interviewer> _interviewers = new();

    public InterviewRota(IEnumerable<string> interviewers)
    {
        foreach (var interviewer in interviewers)
        {
            _interviewers.Add(new Interviewer(interviewer));
        }
    }

    public InterviewRota(IEnumerable<(string name, InterviewLevel level)> interviewers)
    {
        foreach (var interviewer in interviewers)
        {
            _interviewers.Add(new Interviewer(interviewer.name, interviewer.level));
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

    public IEnumerable<string> GetNextInterviewers(int numberOfInterviewers)
    {
        List<string> interviewers = new();
        for (var i = 0; i < numberOfInterviewers; ++i)
        {
            interviewers.Add(GetNextInterviewer(minInterviewLevel: i == 0 ? InterviewLevel.Main : InterviewLevel.Junior));
        }
        
        return interviewers;
    }
    
    public string GetNextInterviewer(int effort = 1, string? language = null, InterviewLevel minInterviewLevel = InterviewLevel.Main)
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