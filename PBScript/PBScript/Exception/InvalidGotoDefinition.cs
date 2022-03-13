namespace PBScript.Exception;

public class InvalidGotoDefinition: PbsException
{
    public InvalidGotoDefinition(string token, int sourceCodeLineNumber) : base("Goto has an invalid (or no) name", token, sourceCodeLineNumber)
    {
    }
}