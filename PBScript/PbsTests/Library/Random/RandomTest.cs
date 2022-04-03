using NUnit.Framework;
using PBScript.Environment.Random;

namespace PbsTexts.Library.Random;

public class RandomTest: TestBase
{
    
    
    protected override string Code => $@"
request pbs/random
request pbs/debug

debug traceOn

var b0 = random.boolean()
var b1 = random()

assert.true(b0 isBoolean && b1 isBoolean)
assert.save(""b"")

var n0 = random.number()

assert.true(n0 isNumber)
assert.save(""n"")
";

    [Test]
    public void Test_CorrectTypesGenerated()
    {
        Assert.True(AssertObject.Results["b"]);
        Assert.True(AssertObject.Results["n"]);
    }

    [Test]
    public void Test_Documentation()
    {
        Assert.NotNull(new RandomObject().GetDocumentation());
    }
    
}