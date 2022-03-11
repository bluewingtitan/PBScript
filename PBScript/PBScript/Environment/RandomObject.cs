using PBScript.Interfaces;

namespace PBScript.Environment;

/// <summary>
/// To create some randomness, this one will create boolean randomness.
///
/// Usage:
/// request with 'request random'
/// use like
/// if random
///     // code goes here...
/// end
/// </summary>
public class RandomObject: IPbsObject
{
    private readonly Random _r = new Random();
    public string GetDocumentation()
    {
        return "Any usage in if-statements will result in a random boolean, Usage as $random will result in a random integer between 0 (inclusive) and 100 (exclusive)";
    }

    public bool ExecuteAction(string command, string parameter, IPbsEnvironment env)
    {
        return _r.Next(0, 2) == 0;
    }
        
    public string GetStringValue()
    {
        return _r.Next(0, 100).ToString();
    }
}