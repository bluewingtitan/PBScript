using NUnit.Framework;
using PBScript.Environment.Random;

namespace PbsTexts.Library.Random;

public class RandomTest: TestBase
{
    
    
    protected override string Code => $@"
request pbs/random

var b0 = random
var b1 = random boolean

assert true b0 is boolean and b1 is boolean
assert save ""b""

var n0 = $random
var n1 = random number

assert true n0 is number and n1 is number
assert save ""n""


assert true random is random
assert save ""type""
";

    [Test]
    public void Test_CorrectTypesGenerated()
    {
        Assert.True(AssertObject.Results["b"]);
        Assert.True(AssertObject.Results["n"]);
        Assert.True(AssertObject.Results["type"]);
    }

    [Test]
    public void Test_Documentation()
    {
        Assert.NotNull(new RandomObject().GetDocumentation());
    }
    
}