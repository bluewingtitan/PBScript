using PBScript.Interfaces;
using PBScript.ProgramElements;

namespace PBScript.Environment;

public class VariableObject : ObjectBase
{
    public PbsValue Value { get; } = new ();
    public VariableType ValueType => Value.ReturnType;

    public VariableObject(string objectName)
    {
        ObjectName = objectName;
    }
    
    public override string GetDocumentation()
    {
        return "Holds different values.";
    }

    public override string ObjectName { get; }

    protected override PbsValue DefaultAction(string command, PbsValue[] param, IPbsEnvironment env)
    {
        if (Value.PbsObjectValue != null)
        {
            return Value.PbsObjectValue.ExecuteAction(command, param, env);
        }
        
        return Value;
    }
}

public enum VariableType
{
    Boolean,
    Number,
    String,
    Undefined,
    PbsObject,
    Null,
    /// <summary>
    /// Special token meant for signaling a potential stop-point for a function-call
    /// </summary>
    Token_FunctionClosure,
}