namespace PBScript.Exception;

public class InvalidRequestException: PbsException
{
    public InvalidRequestException(string token, int sourceCodeLineNumber) : base("Invalid Request: No or faulty request value", token, sourceCodeLineNumber)
    {
    }
}