using PBScript.ExpressionParsing;
using PBScript.Fruity.FruityVm;

namespace PBScript.Fruity.Operations.ValueModifying;

public class Push: IOperation
{
    public string Operator { get; } = "psh";
    public void Execute(ExecutionContext results, IRuntime runtime, IEnvironment environment, string arguments)
    {
        var container = new ExpressionContainer
        {
            Expression = arguments
        };
        
        
        if (arguments.StartsWith('"'))
        {
            ValueHelper.ReadStringLiteral(container, arguments.Length - 1, 1, out var _);
        }
    }

    public string PrepareArguments(string arguments)
    {
        return arguments.Trim();
    }
}