using PBScript.Fruity.FruityVm;
using PBScript.Interfaces;

namespace PBScript.Fruity.Operations.ValueModifying;

public class Def: IOperation
{
    public string Operator { get; } = "def";
    
    public void Execute(ExecutionContext results, IRuntime runtime, IEnvironment environment, string arguments)
    {
        environment.RegisterScoped(PbsValue.Null, arguments);
    }
    
    public string PrepareArguments(string arguments)
    {
        return arguments;
    }

}