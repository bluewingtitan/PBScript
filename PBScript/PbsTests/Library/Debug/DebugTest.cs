using NUnit.Framework;

namespace PbsTexts.Library.Debug;

public class DebugTest: TestBase
{
    private const string DebugTrue = "debugTrue";
    private const string Debug = "debug";
    private const string DebugTraceOn = "debugTraceOn";
    private const string DebugTraceOff = "debugTraceOff";
    
    protected override string Code => $@"
request pbs/debug

// true equals correct execution, as debug only returns true if called
// without action (""""), or with an existing, available action.

assert true debug
assert save ""{Debug}""

assert true debug true
assert save ""{DebugTrue}""

assert true debug traceOn
assert save ""{DebugTraceOn}""

assert true debug traceOff
assert save ""{DebugTraceOff}""";


    [Test]
    public void Test_CorrectExecution()
    {
        Assert.True(AssertObject.Results[Debug]);
        Assert.True(AssertObject.Results[DebugTrue]);
        Assert.True(AssertObject.Results[DebugTraceOn]);
        Assert.True(AssertObject.Results[DebugTraceOff]);
    }
}