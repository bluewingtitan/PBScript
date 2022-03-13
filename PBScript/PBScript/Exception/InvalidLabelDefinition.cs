namespace PBScript.Exception;

public class InvalidLabelDefinition: PbsException
{
    public InvalidLabelDefinition(string token, int sourceCodeLineNumber) : base("Label has an invalid (or no) name", token, sourceCodeLineNumber)
    {
    }
}