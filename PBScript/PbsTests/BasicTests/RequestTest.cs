using NUnit.Framework;

namespace PbsTexts.BasicTests;

public class RequestTest: TestBase
{
    protected override string Code => @"//comment
request pbs/debug
request pbs/random";

    [Test]
    public void Test_RequestedObjectsAttached()
    {
        var debug = Environment.GetObject("debug");
        var random = Environment.GetObject("random");
        
        Assert.NotNull(debug);
        Assert.NotNull(random);
    }
    
}