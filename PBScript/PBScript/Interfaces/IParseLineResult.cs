namespace PBScript.Interfaces;

public interface IParseLineResult
{
    public bool IsBlockStart { get; }
    public bool IsBlockEnd { get; }
}