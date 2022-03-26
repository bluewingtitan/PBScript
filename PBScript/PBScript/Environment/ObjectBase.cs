using System.Text.RegularExpressions;
using PBScript.Interfaces;
using PBScript.Interpretation;
using PBScript.ProgramElements;

namespace PBScript.Environment;

public abstract class ObjectBase: IPbsObject
{
    
    protected delegate PbsValue CommandDelegateTyped(PbsValue[] value, IPbsEnvironment env);
    private readonly Dictionary<string, CommandDelegateTyped> _typedCommands = new();

    public ObjectBase()
    {}
    
    protected abstract PbsValue DefaultAction(PbsValue[] param);

    protected void RegisterTyped(string name, CommandDelegateTyped command) => _typedCommands[name] = command;
    public abstract string GetDocumentation();
    public abstract string ObjectName { get; }
    public PbsValue ExecuteAction(string command, PbsValue[] value, IPbsEnvironment env)
    {
        if (PbsInterpreter.Log)
            env.Log(ObjectName, $"run {command}({String.Concat(value.Select(x=>x.AsString()).ToList())})");

        command = command.Trim();

        if (_typedCommands.ContainsKey(command))
        {
            return _typedCommands[command](value, env);
        }
        
        return DefaultAction(value);
    }
}