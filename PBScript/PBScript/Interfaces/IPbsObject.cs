namespace PBScript.Interfaces;

public interface IPbsObject
{
    public string GetDocumentation();
    
    public string ObjectName { get; }
        
    public IPbsValue ExecuteAction(string command, string parameter, IPbsEnvironment env);

    public string GetStringValue();

}