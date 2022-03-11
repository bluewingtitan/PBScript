namespace PBScript.Exception;

public class InvalidLineException: PbsException
{
    public InvalidLineException(string line, int sourceCodeLineNumber) : base("Line is not valid PBScript", line, sourceCodeLineNumber)
    {
    }
}