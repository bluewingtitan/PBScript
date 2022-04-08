using PBScript.Fruity.FruityVm;

namespace PBScript.Fruity.Operations.ValueModifying;

public class PushVar: IOperation
{
    public string Operator { get; } = "pvr";
    public void Execute(ExecutionContext results, IRuntime runtime, IEnvironment environment, string arguments)
    {
        environment.CurrentStack.Push(environment.GetValue(arguments));
    }

    public string PrepareArguments(string arguments)
    {
        return arguments;
    }
}