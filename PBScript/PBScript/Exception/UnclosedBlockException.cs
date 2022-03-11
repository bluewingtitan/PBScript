namespace PBScript.Exception;

public class UnclosedBlockException: PbsException
{
    public UnclosedBlockException(string token, int sourceCodeLineNumber) : base("Unclosed block detected", token, sourceCodeLineNumber) {}
}