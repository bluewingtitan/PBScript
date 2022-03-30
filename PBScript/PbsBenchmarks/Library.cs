namespace PbsBenchmarks;

public static class Library
{
    public static string Fibonacci(int repetitions = 100) =>@$"// Does fibonacci with a stack.
// create stack
request pbs/stack
var fib = stack.create()

fib.push(0)
fib.push(1)

while fib.count() < {repetitions}
    var b = fib.pop()
    var a = fib.pop()
    var c = a + b 

    fib.push(a)
    fib.push(b)
    fib.push(c)
end";
}