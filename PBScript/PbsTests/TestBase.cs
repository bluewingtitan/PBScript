using NUnit.Framework;
using PBScript.Environment;
using PBScript.Interpretation;
using PbsTexts.TestObjects;

namespace PbsTexts;

public abstract class TestBase
{
    protected abstract string Code { get; }
    
    protected PbsInterpretationResults _program;
    protected PbsEnvironment _environment;
    protected TestCounter _testCounter;

    private const string CounterKey = "counter";
    
    // -2, because it has to run before executing!
    [OneTimeSetUp]
    public void Test_ProgramCompiles()
    {
        Assert.DoesNotThrow(() => _program = PbsInterpreter.InterpretProgram(Code));
        Assert.DoesNotThrow(() => _environment = new PbsEnvironment());
        _testCounter = new TestCounter();
        Assert.DoesNotThrow(() => _environment.RegisterObject(CounterKey, _testCounter, true));

        Test_ProgramRuns();
    }

    private void Test_ProgramRuns()
    {
        var runtime = _program.GetRuntime(_environment);
        
        Assert.DoesNotThrow(runtime.ExecuteAll);
        Assert.NotNull(_environment.GetObject(CounterKey));
    }
}