using PBScript.Interfaces;

namespace PbsTexts.TestObjects;

/// <summary>
/// Simply counts how often an action on this object was called.
/// </summary>
public class TestCounter: IPbsObject
{
    public string ObjectName => "counter";
    public int Counter { get; private set; }
    public IPbsValue ExecuteAction(string command, string parameter, IPbsEnvironment env)
    {
        Counter++;
        return PbsValue.True;
    }

    public string GetStringValue()
    {
        return Counter.ToString();
    }
}