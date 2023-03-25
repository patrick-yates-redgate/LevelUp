using FluentAssertions;

namespace RockPaperScissorsKata;

public class RockPaperScissorsTests
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
    
    public enum RockPaperScissorsLizardSpockOrderedEnum
    {
        Draw = 0,
        Rock = 1,
        Paper = 2,
        Scissors = 3,
        Lizard = 5,
        Spock = 4
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
    
    [TestCase(RockPaperScissorsLizardSpockEnum.Paper, RockPaperScissorsLizardSpockEnum.Rock)]
    [TestCase(RockPaperScissorsLizardSpockEnum.Rock, RockPaperScissorsLizardSpockEnum.Scissors)]
    [TestCase(RockPaperScissorsLizardSpockEnum.Scissors, RockPaperScissorsLizardSpockEnum.Paper)]
    [TestCase(RockPaperScissorsLizardSpockEnum.Lizard, RockPaperScissorsLizardSpockEnum.Paper)]
    [TestCase(RockPaperScissorsLizardSpockEnum.Lizard, RockPaperScissorsLizardSpockEnum.Spock)]
    [TestCase(RockPaperScissorsLizardSpockEnum.Spock, RockPaperScissorsLizardSpockEnum.Rock)]
    [TestCase(RockPaperScissorsLizardSpockEnum.Spock, RockPaperScissorsLizardSpockEnum.Scissors)]
    public void SimpleRockPaperScissorsLizardSpockEnumTests_LeftSideWinsWithNewValuesAndOrderedList(RockPaperScissorsLizardSpockEnum leftSide, RockPaperScissorsLizardSpockEnum rightSide)
    {
        var choices = new RpsChoices<RockPaperScissorsLizardSpockEnum>(leftSide, rightSide);
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsLizardSpockEnum>();
        
        rockPaperScissors.Play(choices).Should().Be(leftSide);
    }
    
    [TestCase(RockPaperScissorsLizardSpockOrderedEnum.Paper, RockPaperScissorsLizardSpockOrderedEnum.Rock)]
    [TestCase(RockPaperScissorsLizardSpockOrderedEnum.Rock, RockPaperScissorsLizardSpockOrderedEnum.Scissors)]
    [TestCase(RockPaperScissorsLizardSpockOrderedEnum.Scissors, RockPaperScissorsLizardSpockOrderedEnum.Paper)]
    [TestCase(RockPaperScissorsLizardSpockOrderedEnum.Lizard, RockPaperScissorsLizardSpockOrderedEnum.Paper)]
    [TestCase(RockPaperScissorsLizardSpockOrderedEnum.Lizard, RockPaperScissorsLizardSpockOrderedEnum.Spock)]
    [TestCase(RockPaperScissorsLizardSpockOrderedEnum.Spock, RockPaperScissorsLizardSpockOrderedEnum.Rock)]
    [TestCase(RockPaperScissorsLizardSpockOrderedEnum.Spock, RockPaperScissorsLizardSpockOrderedEnum.Scissors)]
    public void SimpleRockPaperScissorsLizardSpockOrderedEnumTests_LeftSideWinsWithOrderedList(RockPaperScissorsLizardSpockOrderedEnum leftSide, RockPaperScissorsLizardSpockOrderedEnum rightSide)
    {
        var choices = new RpsChoices<RockPaperScissorsLizardSpockOrderedEnum>(leftSide, rightSide);
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsLizardSpockOrderedEnum>();
        
        rockPaperScissors.Play(choices).Should().Be(leftSide);
    }
}