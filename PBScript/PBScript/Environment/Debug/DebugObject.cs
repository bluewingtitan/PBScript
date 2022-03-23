using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.Environment.Default;

public class DebugObject: IPbsObject
{
    public string ObjectName => "debug";

    public string GetDocumentation()
    {
        return @"Offers basic debug functionality.
COMMANDS:
    log [parameter(s)] logs a simple string containing all parameters.
    trace-(on/off) Activates/Deactivates trace mode
    true simply returns true. does nothing else.";
    }

    public IPbsValue ExecuteAction(string command, string parameter, IPbsEnvironment env)
    {
        switch (command.Trim())
        {
            case "log":
                Console.WriteLine(parameter);
                break;
            
            case "trace-on":
                PbsInterpreter.Log = true;
                break;
            
            case "trace-off":
                PbsInterpreter.Log = false;
                break;


            case "true":
                return PbsValue.True;
                
            default:
                Console.WriteLine($"debug received command '{command}' with parameter '{parameter}'.");
                break;
        }

        return PbsValue.False;
    }
        
    public string GetStringValue()
    {
        return "";
    }
}