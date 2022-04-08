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
    
    /// <summary>
    /// Translated by hand to FruitIL to compare performance with fully-interpreted PBScript
    /// </summary>
    public static string FibonacciFruit(int repetitions = 100) =>@$"rqst pbs/stack
rqst pbs/print
def fib
vpush fib
lock
vpush stack
act create
op =
pop

lock
push 0
vpush fib
act push
pop

lock
push 1
vpush fib
act push
pop

lbl w0
clear
lock
vpush fib
act count
push 100
op <
jif w0b
jmp w0e

lbl w0b
scp

def a
vpush a
lock
vpush fib
act pop
op =
clear

def b
vpush b
lock
vpush fib
act pop
op =
clear

def c
vpush c
vpush a
vpush b
op +
op =
clear

vpush c
vpush print
act out
clear


vpush c
lock
vpush b
lock
vpush a
vpush fib
act push
pop
vpush fib
act push
pop
vpush fib
act push
pop

uscp
jmp w0
lbl w0e";
}