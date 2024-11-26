// See https://aka.ms/new-console-template for more information
var _taskHolder = new A();

Console.WriteLine("Elided stack trace (not awaited):");
Console.WriteLine(_taskHolder.Method().GetAwaiter().GetResult());

Console.WriteLine("Elided stack trace:");
Console.WriteLine(await _taskHolder.Method());
Console.WriteLine();
Console.WriteLine("Async stack trace:");
Console.WriteLine(await _taskHolder.MethodAsync());

public class A
{
    private readonly B _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class B
{
    private readonly C _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class C
{
    private readonly D _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class D
{
    private readonly E _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class E
{
    private readonly F _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class F
{
    private readonly G _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class G
{
    private readonly H _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class H
{
    private readonly I _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class I
{
    private readonly J _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class J
{
    private readonly K _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class K
{
    private readonly L _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class L
{
    private readonly M _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class M
{
    private readonly N _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class N
{
    private readonly O _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class O
{
    private readonly P _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class P
{
    private readonly Q _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class Q
{    
    private readonly R _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class R
{
    private readonly S _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class S
{
    private readonly T _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class T
{
    private readonly U _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class U
{
    private readonly V _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class V
{
    private readonly W _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class W
{
    private readonly X _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class X
{
    private readonly Y _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}

public class Y
{
    private readonly Z _inner = new();
    
    public Task<string> Method()
    {
        return _inner.Method();
    }
    
    public async Task<string> MethodAsync()
    {
        return await _inner.MethodAsync();
    }
}
public class Z
{
    public Task<string> Method() => MethodAsync();
    
    public async Task<string> MethodAsync()
    {
        string before = Environment.StackTrace;// try { throw new Exception("Before"); } catch(Exception ex) { before = ex.ToString(); }
        string beforeException;  try { throw new Exception("Before"); } catch(Exception ex) { beforeException = ex.ToString(); }
        
        await Task.Delay(1);
        string after = Environment.StackTrace; // try { throw new Exception("After"); } catch(Exception ex) { after = ex.ToString(); }
        string afterException; try { throw new Exception("After"); } catch(Exception ex) { afterException = ex.ToString(); }

        return $"""
                before:{before}
                
                before(exception:{beforeException}
                
                after:{after}
                
                after(exception):{afterException}";
                
                """;
    }
}