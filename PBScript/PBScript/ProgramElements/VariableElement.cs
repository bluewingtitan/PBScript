using System.Text.RegularExpressions;
using PBScript.Environment;
using PBScript.Exception;
using PBScript.ExpressionParsing.Exceptions;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ProgramElements;

public class VariableElement: ElementBase
{
    private string _varName = "";
    private IPbsAction? _action;
    public override string Token { get; } = "var";
    
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
        
        if (_action == null)
        {
            throw new NotProperlyInitializedException(Token);
        }

        try
        {

            _action?.Execute(env);
        }
        catch (ExpressionParsingException e)
        {
            throw new PbsException(e.Reason, LineText, SourceCodeLineNumber);
        }
        
        return LineIndex + 1;
    }

    public override void ThrowIfNotValid()
    {
        
    }
    
    public override void ParseLine(string code, int lineIndex, int sourceCodeLineNumber)
    {
        base.ParseLine(code, lineIndex, sourceCodeLineNumber);
        var actionCode = "";
        
        actionCode = code.Split(Token,2)[1].Trim();
        
        try
        {
            _action = new PbsAction(actionCode);
        }
        catch (ExpressionParsingException e)
        {
            throw new PbsException(e.Reason, e.OperatorOrToken, sourceCodeLineNumber);
        }
        
        var matches = Regex.Matches(actionCode, PbsInterpreter.TokenRegex);
        
        if (matches.Count == 0 || matches[0].Index > 0)
        {
            throw new InvalidVariableInitialization(LineText, SourceCodeLineNumber);
        }

        _varName = matches[0].Value;
    }
}