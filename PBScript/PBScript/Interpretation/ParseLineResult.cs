using PBScript.Interfaces;

namespace PBScript.Interpretation;

public class ParseLineResult: IParseLineResult
{
    public ParseLineResult(){}
    public ParseLineResult(bool isBlockStart, bool isBlockEnd)
    {
        IsBlockStart = isBlockStart;
        IsBlockEnd = isBlockEnd;
    }

    public bool IsBlockStart { get; init; } = false;
    public bool IsBlockEnd { get; init; } = false;
}