using NUnit.Framework;
using System.ComponentModel;
using FluentAssertions;

namespace RockPaperScissorsKata;

[TestFixture]
public class VictoryConditionTests
{
    public enum RockPaperScissorsEnum
    {
        Draw = 0,
        Rock,
        Paper,
        Scissors
    }
    
    public enum RockPaperScissorsWithVictoryConditionsEnum
    {
        Draw = 0,
        
        [WinsAgainst<RockPaperScissorsWithVictoryConditionsEnum>(Scissors, "crushes")]
        Rock,
        
        [WinsAgainst<RockPaperScissorsWithVictoryConditionsEnum>(Rock, "covers")]
        Paper,
        
        [WinsAgainst<RockPaperScissorsWithVictoryConditionsEnum>(Paper, "cuts")]
        Scissors
    }
    
    public enum RockPaperScissorsLizardSpockEnum
    {
        Draw = 0,
        
        [WinsAgainst<RockPaperScissorsLizardSpockEnum>(Scissors, "crushes")]
        [WinsAgainst<RockPaperScissorsLizardSpockEnum>(Lizard, "smoushes")]
        Rock,
        
        [WinsAgainst<RockPaperScissorsLizardSpockEnum>(Rock, "wraps")]
        [WinsAgainst<RockPaperScissorsLizardSpockEnum>(Spock, "disproves")]
        Paper,
        
        [WinsAgainst<RockPaperScissorsLizardSpockEnum>(Paper, "cuts")]
        [WinsAgainst<RockPaperScissorsLizardSpockEnum>(Lizard, "decapitates")]
        Scissors,
        
        [WinsWith("vaporises")]
        Spock,
        
        [WinsAgainst<RockPaperScissorsLizardSpockEnum>(Paper, "eats")]
        [WinsAgainst<RockPaperScissorsLizardSpockEnum>(Spock, "poisons")]
        Lizard
    }
    
    [Test]
    public void TestThatVictoryConditionByDefaultIsBeatsWhenNoSpecifiedVictoryConditionsAndNotDrawOrSame([Values] RockPaperScissorsEnum winner, [Values] RockPaperScissorsEnum loser)
    {
        if (winner == loser || winner == RockPaperScissorsEnum.Draw || loser == RockPaperScissorsEnum.Draw)
            Assert.Ignore();
        
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsEnum>();

        rockPaperScissors.VictoryCondition(winner, loser).Should().Be("beats");
    }
    
    [TestCase(RockPaperScissorsWithVictoryConditionsEnum.Rock, RockPaperScissorsWithVictoryConditionsEnum.Scissors, "crushes")]
    [TestCase(RockPaperScissorsWithVictoryConditionsEnum.Paper, RockPaperScissorsWithVictoryConditionsEnum.Rock, "covers")]
    [TestCase(RockPaperScissorsWithVictoryConditionsEnum.Scissors, RockPaperScissorsWithVictoryConditionsEnum.Paper, "cuts")]
    public void TestThatVictoryConditionTakesTheEnumValuesGivenByWinsAgainst(RockPaperScissorsWithVictoryConditionsEnum winner, RockPaperScissorsWithVictoryConditionsEnum loser, string victoryCondition)
    {
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsWithVictoryConditionsEnum>();

        rockPaperScissors.VictoryCondition(winner, loser).Should().Be(victoryCondition);
    }
    
    
    [TestCase(RockPaperScissorsLizardSpockEnum.Rock, RockPaperScissorsLizardSpockEnum.Scissors, "crushes")]
    [TestCase(RockPaperScissorsLizardSpockEnum.Paper, RockPaperScissorsLizardSpockEnum.Rock, "wraps")]
    [TestCase(RockPaperScissorsLizardSpockEnum.Scissors, RockPaperScissorsLizardSpockEnum.Paper, "cuts")]
    [TestCase(RockPaperScissorsLizardSpockEnum.Rock, RockPaperScissorsLizardSpockEnum.Lizard, "smoushes")]
    [TestCase(RockPaperScissorsLizardSpockEnum.Paper, RockPaperScissorsLizardSpockEnum.Spock, "disproves")]
    [TestCase(RockPaperScissorsLizardSpockEnum.Scissors, RockPaperScissorsLizardSpockEnum.Lizard, "decapitates")]
    [TestCase(RockPaperScissorsLizardSpockEnum.Spock, RockPaperScissorsLizardSpockEnum.Rock, "vaporises")]
    [TestCase(RockPaperScissorsLizardSpockEnum.Lizard, RockPaperScissorsLizardSpockEnum.Paper, "eats")]
    [TestCase(RockPaperScissorsLizardSpockEnum.Lizard, RockPaperScissorsLizardSpockEnum.Spock, "poisons")]
    public void TestThatVictoryConditionTakesTheEnumValuesGivenByWinsAgainstOrWinsWith(RockPaperScissorsLizardSpockEnum winner, RockPaperScissorsLizardSpockEnum loser, string victoryCondition)
    {
        var rockPaperScissors = new RockPaperScissors<RockPaperScissorsLizardSpockEnum>();

        rockPaperScissors.VictoryCondition(winner, loser).Should().Be(victoryCondition);
    }
}