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
        yield return Given(Board, "").Then(Width).Should().Be(0);
        yield return Given(Board, "").Then(Height).Should().Be(0);
        yield return Given(Board, "_").Then(Width).Should().Be(1);
        yield return Given(Board, "_").Then(Height).Should().Be(1);
        yield return Given(Board, "_").Then(Values)!.Should().BeEquivalentTo(new[] { new[] { false } });
        yield return Given(Board, "_").When(Value, (0, 0)).Should().BeFalse();
        yield return Given(Board, "X").When(Value, (0, 0)).Should().BeTrue();
        yield return Given(Board, "X_,__").When(Value, (0, 0)).Should().BeTrue();
        yield return Given(Board, "X_,__").Then(Width).Should().Be(2);
        yield return Given(Board, "X_,__").Then(Height).Should().Be(2);
        yield return Given(Board, "_,_,_").Then(Width).Should().Be(1);
        yield return Given(Board, "_,_,_").Then(Height).Should().Be(3);
        yield return Given(Board, "___").Then(Width).Should().Be(3);
        yield return Given(Board, "___").Then(Height).Should().Be(1);
        yield return Given(Board, "__,_X").When(Value, (0, 0)).Should().BeFalse();
        yield return Given(Board, "__,_X").When(Value, (0, 1)).Should().BeFalse();
        yield return Given(Board, "__,_X").When(Value, (1, 0)).Should().BeFalse();
        yield return Given(Board, "__,_X").When(Value, (1, 1)).Should().BeTrue();
        yield return Given(Board, "_X,__").When(Value, (1, 0)).Should().BeTrue();
    }
    
    /*
    [FluentTestCases]
    public static IEnumerable<FluentTestContextBase> BoardTests()
    {
        yield return Given(Board, "X").When(CellNeighbours, 0, 0).Should().Be(0);
        yield return Given(Board, "X_").When(CellNeighbours, 0, 0).Should().Be(0);
        yield return Given(Board, "X_").When(CellNeighbours, 1, 0).Should().Be(1);
        yield return Given(Board, "_X_").When(CellNeighbours, 0, 0).Should().Be(1);
        yield return Given(Board, "_X_").When(CellNeighbours, 1, 0).Should().Be(0);
        yield return Given(Board, "_X_").When(CellNeighbours, 2, 0).Should().Be(1);
        yield return Given(Board, "_,X,_").When(CellNeighbours, 0, 0).Should().Be(1);
        yield return Given(Board, "_,X,_").When(CellNeighbours, 0, 1).Should().Be(0);
        yield return Given(Board, "_,X,_").When(CellNeighbours, 0, 2).Should().Be(1);
        yield return Given(Board, "X_X").When(CellNeighbours, 1, 0).Should().Be(2);
        yield return Given(Board, "X,_,X").When(CellNeighbours, 0, 1).Should().Be(2);
        yield return Given(Board, "___,___,___").When(CellNeighbours, 1, 1).Should().Be(0);
        yield return Given(Board, "XXX,XXX,XXX").When(CellNeighbours, 1, 1).Should().Be(8);
    }
    */

    [FluentTestCases]
    public static IEnumerable<FluentTestContextBase> CounterTest()
    {
        yield return Given(Counter).When(CountNeighbours, new[] { true }).Should().Be(1);
        yield return Given(Counter).When(CountNeighbours, new[] { false }).Should().Be(0);
        yield return Given(Counter).When(CountNeighbours, new[] { true, true }).Should().Be(2);
        yield return Given(Counter).When(CountNeighbours, new[] { false, true }).Should().Be(1);
    }

    [FluentTestCases]
    public static IEnumerable<FluentTestContextBase> NeighbourIteratorTest()
    {
        //TODO: Sort the lists so doesn't have to be ordered in any way
        yield return Given(NeighbourIterator).When(GetNeighbours, (0, 0))!.Should().BeEquivalentTo(new[]
            { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) });
        yield return Given(NeighbourIterator).When(GetNeighbours, (10, 10))!.Should().BeEquivalentTo(new[]
            { (9, 9), (9, 10), (9, 11), (10, 9), (10, 11), (11, 9), (11, 10), (11, 11) });
    }

    [FluentTestCases]
    public static IEnumerable<FluentTestContextBase> CellStateTest()
    {
        yield return Given(CellState, new Dictionary<(int x, int y), bool> { {(0, 0), false} }).When(GetCellStates, new [] { (0,0) }).Should().BeEquivalentTo(new []{false});
    }
}