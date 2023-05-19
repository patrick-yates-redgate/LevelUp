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
        
        var thirdInterviewer = interviewRota.GetNextInterviewer();
        thirdInterviewer.Should().BeOneOf("Alice", "Bob");
        interviewRota.GetNextInterviewer().Should().BeOneOf("Alice", "Bob").And.NotBe(thirdInterviewer);
    }

    [TestCase(new[] { "Alice", "Bob"}, new[]{2, 1, 1}, new[] { "Alice", "Bob", "Bob"})]
    [TestCase(new[] { "Alice", "Bob"}, new[]{2, 3, 2, 1}, new[] { "Alice", "Bob", "Alice", "Bob"})]
    public void SimpleListOf2Interviewers_WithDifferentEfforts_WeReturnTheLowestEffort(string[] names, int[] efforts, string[] expectedOrder)
    {
        var interviewRota = new InterviewRota(names);

        for (var i = 0; i < efforts.Length; ++i)
        {
            interviewRota.GetNextInterviewer(efforts[i]).Should().Be(expectedOrder[i]);
        }
    }
}