namespace PBScript.Interfaces;

public interface IPbsObject
{
    public string GetDocumentation();
    
    public string ObjectName { get; }
    
    public PbsValue ExecuteAction(string command, PbsValue[] value, IPbsEnvironment env);


}