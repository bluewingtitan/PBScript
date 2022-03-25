using NUnit.Framework;
using PBScript.Environment;

namespace PbsTexts.BasicTests;

public class StaticValueTest: TestBase
{
    protected override string Code => $@"
request pbs/debug
debug traceOn
assert true (true is boolean) and $true
assert save ""0""

assert true (false is boolean) and (not false)
assert save ""1""

assert true null is null
assert save ""2""
";


    [Test]
    public void Test_CorrectTypes()
    {
        Assert.True(AssertObject.Results["0"]);
        Assert.True(AssertObject.Results["1"]);
        Assert.True(AssertObject.Results["2"]);
    }

    [Test]
    public void Test_Documentation()
    {
        Assert.NotNull(PbsEnvironment.ProductionReady().GetObject("true")?.GetDocumentation());
    }
    
}