using FluentAssertions;

namespace RockPaperScissorsKata;

public class RockPaperScissorsOutcomeMessageTests
{
    public enum RockPaperScissorsEnum
    {
        Draw = 0,
        Rock,
        Paper,
        Scissors
    }

    private readonly Dictionary<RockPaperScissorsEnum, string>? _victoryMessages = new()
    {
        {RockPaperScissorsEnum.Rock, "crushes"},
        {RockPaperScissorsEnum.Scissors, "cuts"},
        {RockPaperScissorsEnum.Paper, "covers"}
    };

    [TestCase(RockPaperScissorsEnum.Rock)]
    [TestCase(RockPaperScissorsEnum.Scissors)]
    [TestCase(RockPaperScissorsEnum.Paper)]
    public void TestDrawMessage(RockPaperScissorsEnum bothSide)
    {
        var choices = new RpsChoices<RockPaperScissorsEnum>(bothSide, bothSide);
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsEnum>();
        
        rockPaperScissors.OutcomeMessage(choices, RockPaperScissorsEnum.Draw).Should().Be("Draw!");
    }
    
    [TestCase(RockPaperScissorsEnum.Paper, RockPaperScissorsEnum.Rock)]
    [TestCase(RockPaperScissorsEnum.Rock, RockPaperScissorsEnum.Scissors)]
    [TestCase(RockPaperScissorsEnum.Scissors, RockPaperScissorsEnum.Paper)]
    public void TestDrawMessage_LeftSideWins(RockPaperScissorsEnum leftSide, RockPaperScissorsEnum rightSide)
    {
        var choices = new RpsChoices<RockPaperScissorsEnum>(leftSide, rightSide);
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsEnum>();
        
        rockPaperScissors.OutcomeMessage(choices, leftSide).Should().Be($"{leftSide} beats {rightSide}!");
    }
    
    [TestCase(RockPaperScissorsEnum.Rock, RockPaperScissorsEnum.Paper)]
    [TestCase(RockPaperScissorsEnum.Scissors,RockPaperScissorsEnum.Rock)]
    [TestCase(RockPaperScissorsEnum.Paper, RockPaperScissorsEnum.Scissors)]
    public void TestDrawMessage_RightSideWins(RockPaperScissorsEnum leftSide, RockPaperScissorsEnum rightSide)
    {
        var choices = new RpsChoices<RockPaperScissorsEnum>(leftSide, rightSide);
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsEnum>();
        
        rockPaperScissors.OutcomeMessage(choices, rightSide).Should().Be($"{rightSide} beats {leftSide}!");
    }
    
    
    [TestCase(RockPaperScissorsEnum.Paper, RockPaperScissorsEnum.Rock, "covers")]
    [TestCase(RockPaperScissorsEnum.Rock, RockPaperScissorsEnum.Scissors, "crushes")]
    [TestCase(RockPaperScissorsEnum.Scissors, RockPaperScissorsEnum.Paper, "cuts")]
    public void TestDrawMessage_LeftSideWins_WithStandardResponses(RockPaperScissorsEnum leftSide, RockPaperScissorsEnum rightSide, string victoryType)
    {
        var choices = new RpsChoices<RockPaperScissorsEnum>(leftSide, rightSide);
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsEnum>(_victoryMessages);
        
        rockPaperScissors.OutcomeMessage(choices, leftSide).Should().Be($"{leftSide} {victoryType} {rightSide}!");
    }
    
    [TestCase(RockPaperScissorsEnum.Rock, RockPaperScissorsEnum.Paper, "covers")]
    [TestCase(RockPaperScissorsEnum.Scissors,RockPaperScissorsEnum.Rock, "crushes")]
    [TestCase(RockPaperScissorsEnum.Paper, RockPaperScissorsEnum.Scissors, "cuts")]
    public void TestDrawMessage_RightSideWins_WithStandardResponses(RockPaperScissorsEnum leftSide, RockPaperScissorsEnum rightSide, string victoryType)
    {
        var choices = new RpsChoices<RockPaperScissorsEnum>(leftSide, rightSide);
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsEnum>(_victoryMessages);
        
        rockPaperScissors.OutcomeMessage(choices, rightSide).Should().Be($"{rightSide} {victoryType} {leftSide}!");
    }
}