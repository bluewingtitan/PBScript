using System.Text.RegularExpressions;
using PBScript.Exception;
using PBScript.Interfaces;

namespace PBScript.ProgramElements;

public class RequestElement: ElementBase
{
    public const string RequestRegex = @"^[a-zA-Z\/\-]+$";
    private string? _toRequest;
    public override string Token { get; } = "request";
    public override int Execute(IPbsEnvironment env)
    {
        if (string.IsNullOrEmpty(_toRequest))
            throw new NotProperlyInitializedException(Token);

        env.Request(_toRequest);
        return LineIndex + 1;
    }

    public override void ThrowIfNotValid()
    {
        if (string.IsNullOrEmpty(_toRequest) || !Regex.IsMatch(_toRequest, RequestRegex))
        {
            throw new InvalidRequestException(LineText, SourceCodeLineNumber);
        }
    }

    public override void ParseLine(string code, int lineIndex, int sourceCodeLineNumber)
    {
        base.ParseLine(code, lineIndex, sourceCodeLineNumber);

        try
        {
            _toRequest = code.Replace(Token, "").Trim().Split(" ",2)[0];
            ThrowIfNotValid();
        }
        catch (System.Exception)
        {
            throw new InvalidRequestException(LineText, SourceCodeLineNumber);
        }
    }
}