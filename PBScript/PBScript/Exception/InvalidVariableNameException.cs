namespace PBScript.Exception;

public class InvalidVariableNameException: PbsException
{
    public InvalidVariableNameException(string token, int sourceCodeLineNumber) : base("Invalid variable name", token, sourceCodeLineNumber)
    {
    }
}