using NUnit.Framework;
using PBScript.Environment;
using PBScript.Interpretation;
using PbsTexts.TestObjects;
#pragma warning disable CS8618

namespace PbsTexts;

public abstract class TestBase
{
    protected abstract string Code { get; }
    
    private PbsInterpretationResults _program;
    protected PbsEnvironment Environment;
    protected TestCounter TestCounter;

    private const string CounterKey = "counter";
    
    // -2, because it has to run before executing!
    [OneTimeSetUp]
    public void Test_ProgramCompiles()
    {
        Assert.DoesNotThrow(() => _program = PbsInterpreter.InterpretProgram(Code));
        Assert.DoesNotThrow(() => Environment = PbsEnvironment.WithAllDefaultRepositories());
        TestCounter = new TestCounter();
        Assert.DoesNotThrow(() => Environment.RegisterObject(TestCounter, true));

        Test_ProgramRuns();
    }

    private void Test_ProgramRuns()
    {
        var runtime = _program.GetRuntime(Environment);
        
        Assert.DoesNotThrow(runtime.ExecuteAll);
        Assert.NotNull(Environment.GetObject(CounterKey));
    }
}