using System.Text.RegularExpressions;
using PBScript.Environment;
using PBScript.Exception;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ProgramElements;

public class VariableElement: ElementBase
{
    private string _varName = "";
    private IPbsAction _action;
    public override string Token { get; protected set; } = "var";
    
    public override int Execute(IPbsEnvironment env)
    {
        env.RegisterObject(_varName, new VariableObject(null, VariableType.Undefined), true);

        _action.Execute(env);
        
        return LineIndex + 1;
    }

    public override bool CheckValid()
    {
        if (string.IsNullOrEmpty(_varName) || !Regex.IsMatch(_varName, PbsInterpreter.TokenRegex))
        {
            throw new InvalidVariableNameException(LineText, SourceCodeLineNumber);
        }
        
        if (_action == null)
        {
            throw new InvalidVariableInitialization(LineText, SourceCodeLineNumber);
        }

        return true;
    }
    
    public override void ParseLine(string code, int lineIndex, int sourceCodeLineNumber)
    {
        base.ParseLine(code, lineIndex, sourceCodeLineNumber);
        var actionCode = "";
        try
        {
            actionCode = code.Split(Token,2)[1].Trim();
        }
        catch (System.Exception _)
        {
            throw new InvalidVariableInitialization(LineText, SourceCodeLineNumber);
        }
        
        var action = new Action(actionCode);
        _action = action;
        _varName = action.ObjectToken;
    }
}