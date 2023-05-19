namespace InterviewRota;

public class InterviewRota
{
    private Dictionary<string, int> interviewersByTotalEffort;

    public InterviewRota(IEnumerable<string> interviewers)
    {
        interviewersByTotalEffort = new();

        foreach (var interviewer in interviewers)
        {
            interviewersByTotalEffort[interviewer] = 0;
        }
    }

    public void RecordInterview(string interviewer, int effort = 1)
    {
        interviewersByTotalEffort[interviewer] += effort;
    }

    public void ReportUnavailable(string interviewer)
    {
        
    }

    public string GetNextInterviewer()
    {
        var orderedPairs = interviewersByTotalEffort.Keys
            .Select(key => (interviewer: key, totalEffort: interviewersByTotalEffort[key]))
            .OrderBy(tuple => tuple.totalEffort);

        var interviewer = orderedPairs.FirstOrDefault();

        return interviewer.interviewer;
    }
}