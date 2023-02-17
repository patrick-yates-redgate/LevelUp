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
    }
}