using System.Text;

namespace AbstractMathTreesTests;

public class AstTests
{
    [Test]
    public void TestSimpleSum()
    {
        var operation = Mathematical.parse("3 6 +")!;

        operation.Method.Should().BeOfType<Add>();
        operation.Left.Value.Should().BeEquivalentTo(new OperandToken(3));
        operation.Right.Value.Should().BeEquivalentTo(new OperandToken(6));
    }
    
    [Test]
    public void TestSimpleSumAndMultiply()
    {
        var operation = Mathematical.parse("3 6 -6 * +")!;

        operation.Method.Should().BeOfType<Add>();
        operation.Left.Value.Should().BeEquivalentTo(new OperandToken(3));
        
        var right = operation.Right.Value as Operation;
        right.Method.Should().BeOfType<Mul>();
        right.Left.Value.Should().BeEquivalentTo(new OperandToken(6));
        right.Right.Value.Should().BeEquivalentTo(new OperandToken(-6));
    }
}