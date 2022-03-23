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
        if (PbsInterpreter.Log)
            env.Log(Token, "-------- Start Action --------");

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

        code = code.Trim();

        var a = new PbsAction(code);
        _action = a;
        Token = a.ObjectToken;
    }
}