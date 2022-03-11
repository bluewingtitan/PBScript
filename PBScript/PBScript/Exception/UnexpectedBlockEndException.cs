namespace PBScript.Exception;

public class UnexpectedBlockEndException: PbsException
{
    public UnexpectedBlockEndException(string token, int sourceCodeLineNumber) : base("Unexpected End of Block", token, sourceCodeLineNumber) {}
}