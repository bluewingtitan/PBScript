using NUnit.Framework;

namespace PbsTexts.LanguageFeatures;

public class RequestTest: TestBase
{
    protected override string Code => @"//comment
request pbs/debug
request pbs/random";

    [Test]
    public void Test_RequestedObjectsAttached()
    {
        var debug = _environment.GetObject("debug");
        var random = _environment.GetObject("random");
        
        Assert.NotNull(debug);
        Assert.NotNull(random);
    }
    
}