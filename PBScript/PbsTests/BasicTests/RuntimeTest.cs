using System.Text.RegularExpressions;
using NUnit.Framework;
using PBScript;
using PBScript.Environment;
using PBScript.Exception;
using PBScript.Interpretation;

namespace PbsTexts.BasicTests;

public class RuntimeTest
{
    [Test]
    public void TestStandardExecution()
    {
        // basically just various filler-code.
        var code = @"request pbs/debug
request pbs/assert
debug traceOn
assert true ""hi"" is ""string""
assert true assert
assert false false
if (0 < 1)
assert true (0 < 1)
elseif (0 < 1)
assert false (0 < 1)
end
debug traceOff";
        PbsInterpretationResults results = PbsInterpreter.InterpretProgram(code);

        Assert.NotNull(results);
        Assert.NotNull(results.Elements);
        Assert.NotZero(results.Elements?.Count??0);
        Assert.NotZero(results.TotalLines);
        Assert.NotZero(results.LinesOfCode);
        // +1, as first line does not start with a linebreak
        Assert.AreEqual(Regex.Matches(code, "\n").Count+1, results.Elements?.Count ?? 0);
        Assert.AreEqual(results.Elements?.Count ?? 0, results.LinesOfCode);
        Assert.AreEqual(results.Elements?.Count ?? 0, results.TotalLines);
        
        PbsRuntime runtime = results.GetRuntime(PbsEnvironment.WithAllDefaultRepositories());
        Assert.NotNull(runtime);
        Assert.False(runtime.IsFinished);
        Assert.DoesNotThrow(runtime.ExecuteNext);
        Assert.False(runtime.IsFinished);
        Assert.DoesNotThrow(runtime.ExecuteAll);
        Assert.True(runtime.IsFinished);
    }
}