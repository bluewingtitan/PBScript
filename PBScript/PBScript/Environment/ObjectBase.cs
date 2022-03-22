using System.Text.RegularExpressions;
using PBScript.Interfaces;
using PBScript.Interpretation;
using PBScript.ProgramElements;

namespace PBScript.Environment;

public abstract class ObjectBase: IPbsObject
{
    protected delegate IPbsValue CommandDelegate(string parameter, IPbsEnvironment env);
    private readonly Dictionary<string, CommandDelegate> _commands = new();
    protected delegate IPbsValue CommandDelegateTyped(IPbsValue value, IPbsEnvironment env);
    private readonly Dictionary<string, CommandDelegateTyped> _typedCommands = new();

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
    protected void RegisterTyped(string name, CommandDelegateTyped command) => _typedCommands[name] = command;

    public abstract string GetDocumentation();
    public abstract string ObjectName { get; }

    public IPbsValue ExecuteAction(string command, string parameter, IPbsEnvironment env)
    {
        var fullCmd = $"{command} {parameter ?? ""}";
        
        if (PbsInterpreter.Log)
        {
            env.Log(GetType().Name, $"run '{fullCmd}'");
        }
        
        command = command.Trim();
        parameter = parameter?.Trim() ?? "0";

        if (_typedCommands.ContainsKey(command))
        {
            var value = new PbsAction($"({parameter})").Execute(env);
            return _typedCommands[command](value, env);
        }
        
            
        return _commands.ContainsKey(command)? _commands[command](parameter, env) : DefaultAction(parameter);
    }

    public abstract string GetStringValue();
}