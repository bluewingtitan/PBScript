using System.Text.RegularExpressions;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.Environment;

public abstract class ObjectBase: IPbsObject
{
    protected delegate IPbsValue CommandDelegate(string parameter, IPbsEnvironment env);
    private readonly Dictionary<string, CommandDelegate> _commands = new Dictionary<string, CommandDelegate>();

    public ObjectBase()
    {
        Register("is", (s,e) => new PbsValue(Is(s)));
        Register("isnot", (s,e) => new PbsValue(!Is(s)));
    }

    protected abstract bool Is(string param);

    protected virtual IPbsValue DefaultAction(string param)
    {
        return PbsValue.Null;
    }

    protected void Register(string name, CommandDelegate command) => _commands[name] = command;

    public abstract string GetDocumentation();

    public IPbsValue ExecuteAction(string command, string parameter, IPbsEnvironment env)
    {
        // guarantee spaces around first number to allow for things like x-=1
        var first = true;
        var fullCmd = $"{command} {parameter ?? ""}";
        
        if (PbsInterpreter.Log)
        {
            env.Log(GetType().Name, $"run '{fullCmd}'");
        }
        
        var parts = Regex.Replace(fullCmd, @"(\d+(\.\d+)?)|(\.\d+)", match =>
        {
            if (!first)
                return match.Value;

            first = false;
            
            var result = match.Value;
            if (!char.IsWhiteSpace(fullCmd[match.Index - 1]))
            {
                result = " " + result;
            }

            var lastChar = match.Index + match.Length;
            if (fullCmd.Length <= lastChar) return result;
            
            if (!char.IsWhiteSpace(fullCmd[lastChar]))
            {
                result = result + "";
            }

            return result;
        }).Split(" ",2);
            
        command = parts[0].Trim();
        parameter = parts[1].Trim();
            
        return _commands.ContainsKey(command)? _commands[command](parameter, env) : DefaultAction(parameter);
    }

    public abstract string GetStringValue();
}