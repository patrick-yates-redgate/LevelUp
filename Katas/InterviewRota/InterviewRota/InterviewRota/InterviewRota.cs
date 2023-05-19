namespace InterviewRota;

public class InterviewRota
{
    private IEnumerable<string> interviewers;
    private IEnumerator<string> interviewEnumerator;

    public InterviewRota(IEnumerable<string> interviewers)
    {
        this.interviewers = interviewers;
        interviewEnumerator = interviewers.GetEnumerator();
    }

    public string GetNextInterviewer()
    {
        if (interviewEnumerator.MoveNext())
        {
            return interviewEnumerator.Current;
        }
        
        interviewEnumerator = interviewers.GetEnumerator();
        interviewEnumerator.MoveNext();
        return interviewEnumerator.Current;
    }
}