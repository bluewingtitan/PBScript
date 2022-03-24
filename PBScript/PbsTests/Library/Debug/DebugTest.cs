using NUnit.Framework;
using PBScript.Environment.Debug;

namespace PbsTexts.Library.Debug;

public class DebugTest: TestBase
{
    private const string DebugTrue = "debugTrue";
    private const string Debug = "debug";
    private const string DebugTraceOn = "debugTraceOn";
    private const string DebugTraceOff = "debugTraceOff";
    private const string DebugLog = "debugLog";
    private const string DebugHaha = "debughaha";
    private const string DebugRaw = "debugRaw";
    
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
assert save ""{DebugTraceOff}""

assert true debug log hi
assert save ""{DebugLog}""

assert false debug haha
assert save ""{DebugHaha}""

assert true $debug
assert save ""{DebugRaw}""";


    [Test]
    public void Test_CorrectExecution()
    {
        Assert.True(AssertObject.Results[Debug]);
        Assert.True(AssertObject.Results[DebugTrue]);
        Assert.True(AssertObject.Results[DebugTraceOn]);
        Assert.True(AssertObject.Results[DebugLog]);
        Assert.True(AssertObject.Results[DebugHaha]);
        Assert.True(AssertObject.Results[DebugRaw]);
    }

    [Test]
    public void Test_Documentation()
    {
        Assert.NotNull(new DebugObject().GetDocumentation());
    }
    
}