using FluentTests;
using FluentTests.Context;
using static FluentTests.FluentTests;
using static GameOfLife.Tests.TestUtils;

namespace GameOfLife.Tests;

[TestFixture]
public class GameOfLifeTests
{
    [FluentTestCasesBase]
    public void RunTest(FluentTestContextBase testStep) => testStep.InvokeTest();

    [FluentTestCases]
    public static IEnumerable<FluentTestContextBase> BasicBoardSetup()
    {
        yield return Given(Board, "").Then("Width", gol => gol.Width).Should().Be(0);
        yield return Given(Board , "").Then("Height", gol => gol.Height).Should().Be(0);
        yield return Given(Board, "_").Then("Width", gol => gol.Width).Should().Be(1);
        yield return Given(Board, "_").Then("Height", gol => gol.Height).Should().Be(1);
        yield return Given(Board, "_").Then("Values", gol => gol.Values)!.Should().BeEquivalentTo(new[] { new[] { false } });
        yield return Given(Board, "_").When(Value, 0, 0).Should().BeFalse();
        yield return Given(Board, "X").When(Value, 0, 0).Should().BeTrue();
        yield return Given(Board, "X_,__").When(Value, 0, 0).Should().BeTrue();
        yield return Given(Board, "X_,__").Then("Width", gol => gol.Width).Should().Be(2);
        yield return Given(Board, "X_,__").Then("Height", gol => gol.Height).Should().Be(2);
        yield return Given(Board, "_,_,_").Then("Width", gol => gol.Width).Should().Be(1);
        yield return Given(Board, "_,_,_").Then("Height", gol => gol.Height).Should().Be(3);
        yield return Given(Board, "___").Then("Width", gol => gol.Width).Should().Be(3);
        yield return Given(Board, "___").Then("Height", gol => gol.Height).Should().Be(1);
        yield return Given(Board, "__,_X").When(Value, 0, 0).Should().BeFalse();
        yield return Given(Board, "__,_X").When(Value, 0, 1).Should().BeFalse();
        yield return Given(Board, "__,_X").When(Value, 1, 0).Should().BeFalse();
        yield return Given(Board, "__,_X").When(Value, 1, 1).Should().BeTrue();
        yield return Given(Board, "_X,__").When(Value, 1, 0).Should().BeTrue();
    }
}