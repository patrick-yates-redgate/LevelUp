namespace InterviewRota;

public class InterviewRota
{
    private readonly Dictionary<string, (int actualEffort, int pendingEffort)> _interviewersByTotalEffort = new();
    private readonly HashSet<string> _unavailableInterviewers = new();

    public InterviewRota(IEnumerable<string> interviewers)
    {
        foreach (var interviewer in interviewers)
        {
            _interviewersByTotalEffort[interviewer] = (0, 0);
        }
    }

    public void RecordInterview(string interviewer, int effort = 1)
    {
        var (actualEffort, pendingEffort) = _interviewersByTotalEffort[interviewer];
        _interviewersByTotalEffort[interviewer] = (actualEffort + effort, pendingEffort - effort);
        
        _unavailableInterviewers.Clear();
    }

    public void ReportUnavailable(string interviewer)
    {
        _unavailableInterviewers.Add(interviewer);
    }

    public string GetNextInterviewer(int effort = 1)
    {
        var orderedPairs = _interviewersByTotalEffort.Keys
            .Select(key => (interviewer: key, totalEffort: _interviewersByTotalEffort[key]))
            .Where(key => !_unavailableInterviewers.Contains(key.interviewer))
            .OrderBy(tuple => tuple.totalEffort.actualEffort + tuple.totalEffort.pendingEffort);

        var interviewer = orderedPairs.FirstOrDefault();
        var (actualEffort, pendingEffort) = _interviewersByTotalEffort[interviewer.interviewer];
        _interviewersByTotalEffort[interviewer.interviewer] = (actualEffort, pendingEffort + effort);

        return interviewer.interviewer;
    }
}