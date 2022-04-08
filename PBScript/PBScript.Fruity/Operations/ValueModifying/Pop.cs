using System.Text.RegularExpressions;
using PBScript.Fruity.FruityVm;

namespace PBScript.Fruity.Operations.ValueModifying;

public class Pop: IOperation
{
    public string Operator { get; } = "pop";
    
    public void Execute(ExecutionContext results, IRuntime runtime, IEnvironment environment, string arguments)
    {
        environment.CurrentStack.Pop();
    }

    public string PrepareArguments(string arguments)
    {
        return "";
    }
}