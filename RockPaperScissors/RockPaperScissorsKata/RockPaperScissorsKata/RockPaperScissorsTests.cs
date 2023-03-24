using FluentAssertions;

namespace RockPaperScissorsKata;

public class Tests
{
    public enum RockPaperScissorsEnum
    {
        Draw = 0,
        Rock,
        Paper,
        Scissors
    }
    
    public enum RockPaperScissorsLizardSpockEnum
    {
        Draw = 0,
        Rock,
        Paper,
        Scissors,
        Spock,
        Lizard
    }
    
    [TestCase(RockPaperScissorsEnum.Paper, RockPaperScissorsEnum.Rock)]
    [TestCase(RockPaperScissorsEnum.Rock, RockPaperScissorsEnum.Scissors)]
    [TestCase(RockPaperScissorsEnum.Scissors, RockPaperScissorsEnum.Paper)]
    public void SimpleRockPaperScissorsTests_LeftSideWins(RockPaperScissorsEnum leftSide, RockPaperScissorsEnum rightSide)
    {
        var choices = new RpsChoices<RockPaperScissorsEnum>(leftSide, rightSide);
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsEnum>();
        
        rockPaperScissors.Play(choices).Should().Be(leftSide);
    }
    
    [TestCase(RockPaperScissorsEnum.Rock, RockPaperScissorsEnum.Paper)]
    [TestCase(RockPaperScissorsEnum.Scissors,RockPaperScissorsEnum.Rock)]
    [TestCase(RockPaperScissorsEnum.Paper, RockPaperScissorsEnum.Scissors)]
    public void SimpleRockPaperScissorsTests_RightSideWins(RockPaperScissorsEnum leftSide, RockPaperScissorsEnum rightSide)
    {
        var choices = new RpsChoices<RockPaperScissorsEnum>(leftSide, rightSide);
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsEnum>();
        
        rockPaperScissors.Play(choices).Should().Be(rightSide);
    }

    [TestCase(RockPaperScissorsEnum.Rock)]
    [TestCase(RockPaperScissorsEnum.Scissors)]
    [TestCase(RockPaperScissorsEnum.Paper)]
    public void SimpleRockPaperScissorsTests_Draw(RockPaperScissorsEnum bothSides)
    {
        var choices = new RpsChoices<RockPaperScissorsEnum>(bothSides, bothSides);
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsEnum>();
        
        rockPaperScissors.Play(choices).Should().Be(RockPaperScissorsEnum.Draw);
    }

    [TestCase(RockPaperScissorsEnum.Draw, RockPaperScissorsEnum.Rock)]
    [TestCase(RockPaperScissorsEnum.Rock,RockPaperScissorsEnum.Draw)]
    public void SimpleRockPaperScissorsTests_ShouldThrowIfDrawIsSelectedByEither(RockPaperScissorsEnum leftSide, RockPaperScissorsEnum rightSide)
    {
        var choices = new RpsChoices<RockPaperScissorsEnum>(leftSide, rightSide);
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsEnum>();
        
        rockPaperScissors.Invoking(x => x.Play(choices)).Should().Throw<ArgumentException>("Please do not select Draw!");
    }
    
    [TestCase(RockPaperScissorsLizardSpockEnum.Paper, RockPaperScissorsLizardSpockEnum.Rock)]
    [TestCase(RockPaperScissorsLizardSpockEnum.Rock, RockPaperScissorsLizardSpockEnum.Scissors)]
    [TestCase(RockPaperScissorsLizardSpockEnum.Scissors, RockPaperScissorsLizardSpockEnum.Paper)]
    public void SimpleRockPaperScissorsLizardSpockEnumTests_LeftSideWinsWithOriginalValues(RockPaperScissorsLizardSpockEnum leftSide, RockPaperScissorsLizardSpockEnum rightSide)
    {
        var choices = new RpsChoices<RockPaperScissorsLizardSpockEnum>(leftSide, rightSide);
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsLizardSpockEnum>();
        
        rockPaperScissors.Play(choices).Should().Be(leftSide);
    }
    
    [TestCase(RockPaperScissorsLizardSpockEnum.Lizard, RockPaperScissorsLizardSpockEnum.Paper)]
    [TestCase(RockPaperScissorsLizardSpockEnum.Lizard, RockPaperScissorsLizardSpockEnum.Spock)]
    [TestCase(RockPaperScissorsLizardSpockEnum.Spock, RockPaperScissorsLizardSpockEnum.Rock)]
    [TestCase(RockPaperScissorsLizardSpockEnum.Spock, RockPaperScissorsLizardSpockEnum.Scissors)]
    public void SimpleRockPaperScissorsLizardSpockEnumTests_LeftSideWinsWithNewValues(RockPaperScissorsLizardSpockEnum leftSide, RockPaperScissorsLizardSpockEnum rightSide)
    {
        var choices = new RpsChoices<RockPaperScissorsLizardSpockEnum>(leftSide, rightSide);
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsLizardSpockEnum>();
        
        rockPaperScissors.Play(choices).Should().Be(leftSide);
    }
}