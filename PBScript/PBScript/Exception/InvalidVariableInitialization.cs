namespace PBScript.Exception;

public class InvalidVariableInitialization: PbsException
{
    public InvalidVariableInitialization(string token, int sourceCodeLineNumber) : base("Invalid initialization of a variable", token, sourceCodeLineNumber)
    {}
}