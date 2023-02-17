using FluentTests;
using FluentTests.Context;
using static FluentTests.FluentTests;
using static GameOfLife.Tests.TestUtils;

namespace GameOfLife.Tests;

[TestFixture]
public class GameOfLifeCellTests
{
    [FluentTestCasesBase]
    public void RunTest(FluentTestContextBase testStep) => testStep.InvokeTest();

    [FluentTestCases]
    public static IEnumerable<FluentTestContextBase> NeighbourCountTests()
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
}