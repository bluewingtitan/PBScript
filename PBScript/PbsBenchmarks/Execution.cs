using BenchmarkDotNet.Attributes;
using PBScript.Environment;
using PBScript.Interpretation;

namespace PbsBenchmarks;

[MemoryDiagnoser]
public class Execution
{
    private readonly PbsInterpretationResults _fib;

    public Execution()
    {
        _fib = PbsInterpreter.InterpretProgram(Library.Fibonacci());
    }

    [Benchmark]
    public void Fibonacci100() => _fib.GetRuntime(PbsEnvironment.ProductionReady()).ExecuteAll();


}