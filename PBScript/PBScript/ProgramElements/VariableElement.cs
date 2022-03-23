using System.Text.RegularExpressions;
using PBScript.Environment;
using PBScript.Exception;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ProgramElements;

public class VariableElement: ElementBase
{
    private string _varName = "";
    private IPbsAction? _action;
    public override string Token { get; protected set; } = "var";
    
    public override int Execute(IPbsEnvironment env)
    {
        if (PbsInterpreter.Log)
        {
            env.Log("var " + _varName, "register ");
        }
        env.RegisterObject(new VariableObject(_varName), true);

        if (PbsInterpreter.Log)
        {
            env.Log("var " + _varName, "set now");
        }
        
        _action?.Execute(env);
        
        return LineIndex + 1;
    }

    public override bool CheckValid()
    {
        return true;
    }
    
    public override void ParseLine(string code, int lineIndex, int sourceCodeLineNumber)
    {
        base.ParseLine(code, lineIndex, sourceCodeLineNumber);
        var actionCode = "";
        try
        {
            actionCode = code.Split(Token,2)[1].Trim();

            if (actionCode.StartsWith("$"))
            {
                actionCode = actionCode.Split("$", 2)[1];
            }
        }
        catch (System.Exception)
        {
            throw new InvalidVariableInitialization(LineText, SourceCodeLineNumber);
        }

        
        var action = new PbsAction(actionCode);
        _action = action;
        _varName = action.ObjectToken;
        if (actionCode.Replace(" ","").Length < _varName.Length + 2 || action.AlwaysFalse)
        {
            throw new InvalidVariableInitialization(LineText, SourceCodeLineNumber);
        }
    }
}