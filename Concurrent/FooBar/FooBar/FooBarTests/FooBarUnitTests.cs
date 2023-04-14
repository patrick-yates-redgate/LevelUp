using FluentAssertions;

namespace FooBarTests;

public class FooBarUnitTests
{
    private string _expectedFooBar = null!;
    
    [SetUp]
    public void SetUp()
    {
        _expectedFooBar = string.Empty;
        
        for (var i = 0; i < 100; ++i)
        {
            _expectedFooBar += "foobar" + Environment.NewLine;
        }
    }

    public static IEnumerable<IFooBar> TestCases()
    {
        yield return new FooBarCallback();
        yield return new FooBarEventWaitHandle();
        yield return new FooBarEventWaitHandleAsync();
        yield return new FooBarSingleEventWaitHandle();
        yield return new FooBarSingleEventWaitHandleBroken();
        yield return new FooBarSingleEventWaitHandleSyncStart();
        yield return new FooBarMutex();
        yield return new FooBarJoin();
        yield return new FooBarSemaphore();
    }
    
    [TestCaseSource(nameof(TestCases))]
    public async Task Test1(IFooBar foobar)
    {
        var output = await foobar.RunAndReturnResult();
        output.Should().Be(_expectedFooBar);
    }
}