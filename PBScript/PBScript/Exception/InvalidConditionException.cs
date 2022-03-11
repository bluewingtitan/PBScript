namespace PBScript.Exception;

public class InvalidConditionException: PbsException
{
    public InvalidConditionException(string token, int sourceCodeLineNumber) : base("Invalid conditional expression", token, sourceCodeLineNumber)
    {
    }
}