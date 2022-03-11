using PBScript.Exception;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ProgramElements;

/// <summary>
/// Any element that does not start with any special language keyword.
/// </summary>
public class ActionElement: ElementBase
{
    private IPbsAction? _action;
    public override string Token { get; protected set; } = "";
    
    public override int Execute(IPbsEnvironment env)
    {
        _action?.Execute(env);
        return LineIndex + 1;
    }


    public override bool CheckValid()
    {
        if (_action == null)
        {
            throw new InvalidLineException(LineText, SourceCodeLineNumber);
        }
        
        return true;
    }

    public override IParseLineResult ParseLine(string code, int lineIndex, int sourceCodeLineNumber)
    {
        base.ParseLine(code, lineIndex, sourceCodeLineNumber);
        
        var a = new Action(code);
        _action = a;
        Token = a.ObjectToken;

        return new ParseLineResult
        {
            IsBlockEnd = false,
            IsBlockStart = false
        };
    }
}