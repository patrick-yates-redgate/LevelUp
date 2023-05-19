using FluentAssertions;

namespace InterviewRota.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SimpleListOf2Interviewer_WillReturnInOrderAndLoop()
    {
        var interviewRota = new InterviewRota(new List<string> {"Alice", "Bob"});
        
        interviewRota.GetNextInterviewer().Should().Be("Alice");
        interviewRota.GetNextInterviewer().Should().Be("Bob");
        interviewRota.GetNextInterviewer().Should().Be("Alice");
    }
}