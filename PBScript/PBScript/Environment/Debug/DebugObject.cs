using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.Environment.Debug;

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
            
            case "traceOn":
                PbsInterpreter.Log = true;
                break;
            
            case "traceOff":
                PbsInterpreter.Log = false;
                break;


            case "true":
                return PbsValue.True;
            
            case "":
            case null:
                return PbsValue.True;
                
            default:
                return PbsValue.False;
        }
        return PbsValue.True;
    }
        
    public string GetStringValue()
    {
        return PbsValue.True.AsString();
    }
}