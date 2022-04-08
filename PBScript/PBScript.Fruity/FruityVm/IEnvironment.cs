using PBScript.Interfaces;
namespace PBScript.Fruity.FruityVm;

public interface IEnvironment
{
    public Stack<PbsValue> CurrentStack { get; }
    public PbsValue GetValue(string key);
    public void RegisterScoped(PbsValue value, string key);
    public void StartScope();
    public void StopScope();
}

/*
public interface IPbsEnvironment
{
    public void Log(string elementName, string message);
    public IPbsObject? GetObject(string key);
    public void RegisterObject(IPbsObject pbsObject, bool @override = false);
    public void DeregisterObject(IPbsObject pbsObject);

    /// <summary>
    /// Requests the Environment to load and attach the defined object from the known sources.
    /// </summary>
    /// <param name="requestedObject">the trimmed key of the requested object</param>
    public void Request(string requestedObject);
}
*/