using System.Text.RegularExpressions;
using PBScript.Exception;
using PBScript.Interfaces;

namespace PBScript.ProgramElements;

public class RequestElement: ElementBase
{
    public const string VariableRegex = @"^[a-zA-Z]+$";
    private string? _toRequest;
    public override string Token { get; protected set; } = "request";
    public override int Execute(IPbsEnvironment env)
    {
        if (!string.IsNullOrEmpty(_toRequest))
            env.Request(_toRequest);

        return LineIndex + 1;
    }

    public override bool CheckValid()
    {
        if (string.IsNullOrEmpty(_toRequest) || !Regex.IsMatch(_toRequest, VariableRegex))
        {
            throw new InvalidRequestException(LineText, SourceCodeLineNumber);
        }

        return true;
    }

    public override void ParseLine(string code, int lineIndex, int sourceCodeLineNumber)
    {
        base.ParseLine(code, lineIndex, sourceCodeLineNumber);

        try
        {
            _toRequest = code.Replace(Token, "").Trim().Split(" ",2)[0];
            CheckValid();
        }
        catch (System.Exception)
        {
            throw new InvalidRequestException(LineText, SourceCodeLineNumber);
        }
    }
}