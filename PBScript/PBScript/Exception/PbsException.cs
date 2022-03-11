namespace PBScript.Exception;

public abstract class PbsException: System.Exception
{
    public int SourceCodeLineNumber { get; }
    public string ErrorDescription { get; }
    public string Token { get; }
    
    protected PbsException(string errorDescription, string token, int sourceCodeLineNumber) : base($"{errorDescription} at '{token}' in line {sourceCodeLineNumber}")
    {
        ErrorDescription = errorDescription;
        Token = token;
        SourceCodeLineNumber = sourceCodeLineNumber;
    }
}