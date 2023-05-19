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

    public string GetNextInterviewer(int effort = 1)
    {
        var orderedPairs = interviewersByTotalEffort.Keys
            .Select(key => (interviewer: key, totalEffort: interviewersByTotalEffort[key]))
            .OrderBy(tuple => tuple.totalEffort);

        var interviewer = orderedPairs.FirstOrDefault();
        interviewersByTotalEffort[interviewer.interviewer] += effort;

        return interviewer.interviewer;
    }
}