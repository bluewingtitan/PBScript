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

        _action?.Execute(env);
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

        var a = new PbsAction(code);
        _action = a;
        _token = a.ObjectToken;
    }
}