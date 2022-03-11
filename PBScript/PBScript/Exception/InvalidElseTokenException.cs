namespace PBScript.Exception;

public class InvalidElseTokenException: PbsException
{
    public InvalidElseTokenException(int sourceCodeLineNumber) : base("Invalid use of Token: Else may only be used in if statements", "else", sourceCodeLineNumber)
    {
    }
}