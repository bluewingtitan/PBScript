using System.Text.RegularExpressions;
using PBScript.Exception;
using PBScript.ExpressionParsing.Exceptions;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ProgramElements;

/// <summary>
/// Any element that does not start with any special language keyword.
/// </summary>
public class ActionElement: ElementBase
{
    private IPbsAction? _action;
    private string _token = "";
    public override string Token => _token;
    
    public override int Execute(IPbsEnvironment env)
    {
        if (PbsInterpreter.Log)
            env.Log(Token, "-------- Start Action --------");

        if (_action == null)
        {
            throw new NotProperlyInitializedException("an action-Element");
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
        if (_action == null)
        {
            throw new NotProperlyInitializedException("an action-Element");
        }
    }

    public override void ParseLine(string code, int lineIndex, int sourceCodeLineNumber)
    {
        base.ParseLine(code, lineIndex, sourceCodeLineNumber);

        code = code.Trim();
        
        var matches = Regex.Matches(code, PbsInterpreter.TokenRegex);
        
        if (matches.Count == 0 || matches[0].Index > 0)
        {
            throw new InvalidLineException(LineText, SourceCodeLineNumber);
        }

        _token = matches[0].Value;
        try
        {
            _action = new PbsAction(code);
        }
        catch (ExpressionParsingException e)
        {
            throw new PbsException(e.Reason, e.OperatorOrToken, sourceCodeLineNumber);
        }
    }
}