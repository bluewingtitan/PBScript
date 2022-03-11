using PBScript.Interfaces;

namespace PbsTexts.TestObjects;

/// <summary>
/// Simply counts how often an action on this object was called.
/// </summary>
public class TestCounter: IPbsObject
{
    public int Counter { get; private set; }
    public bool ExecuteAction(string command, string parameter, IPbsEnvironment env)
    {
        Counter++;
        return true;
    }

    public string GetStringValue()
    {
        return Counter.ToString();
    }
}