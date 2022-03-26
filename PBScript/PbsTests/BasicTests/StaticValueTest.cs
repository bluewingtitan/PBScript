using NUnit.Framework;
using PBScript.Environment;

namespace PbsTexts.BasicTests;

public class StaticValueTest: TestBase
{
    protected override string Code => $@"
request pbs/debug
debug traceOn
assert true( (true is boolean) && true)
assert save ""0""

assert true ((false isBoolean) && (!false))
assert save ""1""

assert true (null isNull)
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