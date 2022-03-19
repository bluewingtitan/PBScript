using PBScript.Interfaces;

namespace PBScript.Environment.Default;

public class DebugObject: IPbsObject
{
    public string GetDocumentation()
    {
        return @"Offers basic debug functionality.
COMMANDS:
    log [parameter(s)] logs a simple string containing all parameters.
    true simply returns true. does nothing else.";
    }

    public bool ExecuteAction(string command, string parameter, IPbsEnvironment env)
    {
        switch (command.Trim())
        {
            case "log":
                Console.WriteLine(parameter);
                break;
                
            case "true":
                return true;
                
            default:
                Console.WriteLine($"debug received command '{command}' with parameter '{parameter}'.");
                break;
        }

        return false;
    }
        
    public string GetStringValue()
    {
        return "";
    }
}