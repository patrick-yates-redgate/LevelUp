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
        var interviewRota = new InterviewRota(new List<string> { "Alice", "Bob" });

        interviewRota.GetNextInterviewer().Should().Be("Alice");
        interviewRota.RecordInterview("Alice");
        interviewRota.GetNextInterviewer().Should().Be("Bob");
        interviewRota.RecordInterview("Bob");

        var thirdInterviewer = interviewRota.GetNextInterviewer();
        interviewRota.RecordInterview(thirdInterviewer);
        thirdInterviewer.Should().BeOneOf("Alice", "Bob");
        interviewRota.GetNextInterviewer().Should().BeOneOf("Alice", "Bob").And.NotBe(thirdInterviewer);
    }

    [TestCase(new[] { "Alice", "Bob" }, new[] { 2, 1, 1 }, new[] { "Alice", "Bob", "Bob" })]
    [TestCase(new[] { "Alice", "Bob" }, new[] { 2, 3, 2, 1 }, new[] { "Alice", "Bob", "Alice", "Bob" })]
    public void SimpleListOf2Interviewers_WithDifferentEfforts_WeReturnTheLowestEffort(string[] names, int[] efforts,
        string[] expectedOrder)
    {
        var interviewRota = new InterviewRota(names);

        for (var i = 0; i < efforts.Length; ++i)
        {
            interviewRota.GetNextInterviewer(efforts[i]).Should().Be(expectedOrder[i]);
            interviewRota.RecordInterview(expectedOrder[i], efforts[i]);
        }
    }

    [Test]
    public void WhenInterviewerIsntAvailableThenSelectTheNextPersonButReturnToThemAfter()
    {
        var interviewRota = new InterviewRota(new List<string> { "Alice", "Bob", "Clive" });

        interviewRota.ReportUnavailable("Alice");
        interviewRota.GetNextInterviewer().Should().Be("Bob");
        interviewRota.RecordInterview("Bob");
        interviewRota.GetNextInterviewer().Should().Be("Alice");
    }

    [Test]
    public void WeCanFilterInterviewersByLanguage()
    {
        var interviewRota = new InterviewRota(new List<string> { "Alice", "Bob", "Clive" });

        interviewRota.RecordInterview("Clive", language: "C#");
        interviewRota.GetNextInterviewer(language: "C#").Should().Be("Clive");
    }

    [Test]
    public void IfYouAreUnavailableAndAreSkippedThenWeSetYouAvailableAgain()
    {
        var interviewRota = new InterviewRota(new List<string> { "Alice", "Bob" });

        interviewRota.ReportUnavailable("Alice");
        interviewRota.GetNextInterviewer().Should().Be("Bob");
        interviewRota.GetNextInterviewer().Should().Be("Alice");
    }

    [Test]
    public void FirstInterviewWeWantAtLeast2PeopleWithOneOfThemMainAtLeast()
    {
        var interviewRota = new InterviewRota(new List<(string name, InterviewLevel level)>
        {
            ("Alice", InterviewLevel.Junior),
            ("Bob", InterviewLevel.Observer),
            ("Clive", InterviewLevel.Main)
        });

        interviewRota.GetNextInterviewers(2).Should().BeEquivalentTo(new []
        {
            "Clive", "Alice"
        });
    }

    /*
    [TestCase("Alice", InterviewLevel.Junior)]
    [TestCase("Bob", InterviewLevel.Observer)]
    [TestCase("Clive", InterviewLevel.Main)]
    public void CanSpecificInterviewerLevelAndRequestThatWhenWeGetNextInterviewer(string name, InterviewLevel level)
    {
        var interviewRota = new InterviewRota(new List<(string name, InterviewLevel level)>
        {
            ("Alice", InterviewLevel.Junior),
            ("Bob", InterviewLevel.Observer),
            ("Clive", InterviewLevel.Main)
        });

        interviewRota.GetNextInterviewer(level: level).Should().Be(name);
    }
    */
}