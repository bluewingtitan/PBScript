using NUnit.Framework;

namespace PbsTexts.RealWorldAlgorithms;

public class FibonacciTest: TestBase
{
    protected override string Code => @"
request pbs/debug
//debug traceOn

" + PbsBenchmarks.Library.Fibonacci(50) + 
                                      @"
// test for last number to be the actual 50th number in the fib-sequence.
assert true fib pop == 7778742049
assert save ""correct""
";


    [Test]
    public void Test_CorrectGeneration()
    {
        Assert.True(AssertObject.Results["correct"]);
    }

}