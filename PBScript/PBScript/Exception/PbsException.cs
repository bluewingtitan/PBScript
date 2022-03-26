namespace PBScript.Exception;

public class PbsException: System.Exception
{
    public int SourceCodeLineNumber { get; }
    public string ErrorDescription { get; }
    public string Token { get; }
    
    public PbsException(string errorDescription, string token, int sourceCodeLineNumber) : base($"{errorDescription} at '{token}' in line {sourceCodeLineNumber}")
    {
        ErrorDescription = errorDescription;
        Token = token;
        SourceCodeLineNumber = sourceCodeLineNumber;
    }
}