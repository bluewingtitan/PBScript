using System.Text.RegularExpressions;
using PBScript.Fruity.FruityVm;

namespace PBScript.Fruity.Operations;

public interface IOperation
{
    public string Operator { get; }
    public void Execute(ExecutionContext results, IRuntime runtime, IEnvironment environment, string arguments);
    public string PrepareArguments(string arguments);
}