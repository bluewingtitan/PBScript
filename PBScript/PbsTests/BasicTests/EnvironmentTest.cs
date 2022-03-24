using NUnit.Framework;
using PBScript.Environment;
using PBScript.Environment.Debug;
using PBScript.Interpretation;
using PbsTexts.TestObjects;

namespace PbsTexts.BasicTests;

public class EnvironmentTest
{
    [Test]
    public void Test_CreationRegistrationDeRegistration()
    {
        PbsInterpreter.Log = true;
        var env = new PbsEnvironment(new[] {new PbsDebugRepository()}, new [] {DefaultRepository.Random});
        env.Request("pbs/debug");
        Assert.DoesNotThrow( () => env.Request("pbs/debug"));
        Assert.NotNull(env.GetObject("debug"));
        
        env.RegisterObject(new TestCounter());
        var counter = env.GetObject("counter");
        Assert.DoesNotThrow(() => env.RegisterObject(counter));
        Assert.NotNull(counter);
        env.DeregisterObject(counter);
        Assert.Null(env.GetObject("counter"));
        
        Assert.DoesNotThrow(() => env.DeregisterObject(counter));
        Assert.DoesNotThrow(() => env.DeregisterObject(null));
        Assert.DoesNotThrow(() => env.RegisterObject(null));
        Assert.Null(env.GetObject("counter"));
        PbsInterpreter.Log = false;
    }
}