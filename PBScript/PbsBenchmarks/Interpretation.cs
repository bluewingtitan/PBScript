using BenchmarkDotNet.Attributes;
using PBScript.Interpretation;

namespace PbsBenchmarks;

[MemoryDiagnoser]
public class Interpretation
{
    #region Interpretation Only Scripts

    private const string Script_TodoGrow = @"// a somewhat minimal script for the game ""TODO:Grow"", powered by PBScript.
// Will not execute correctly in this context, but will interpret correctly.
request grow/mailman
request pbs/time/hour

var plant = ""salad""

if hour%2==0
    plant = ""carrot""
end

while i moved
    while i moved
        i harvest
        i plant plant
        i move ""right""
    end

    i move ""down""

    while i moved
        i harvest
        i plant plant
        i move ""left""
    end

    i move ""down""
end";

    #endregion
    
    [Benchmark]
    public PbsInterpretationResults TodoGrowScript() => PbsInterpreter.InterpretProgram(Script_TodoGrow);
    
    [Benchmark] 
    public PbsInterpretationResults FibonacciScript() => PbsInterpreter.InterpretProgram(Library.Fibonacci());
}