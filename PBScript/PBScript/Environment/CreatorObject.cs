using System.Text.RegularExpressions;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.Environment;

public class CreatorObject: ObjectBase
{
    public delegate IPbsObject CreateObjectDelegate(string s);

    private readonly CreateObjectDelegate _creator;
    
    public CreatorObject(string objectName, CreateObjectDelegate creator)
    {
        ObjectName = objectName;
        _creator = creator;

        RegisterTyped("create", Create);
    }

    private IPbsValue Create(IPbsValue v, IPbsEnvironment env)
    {
        if (v.StringValue != null)
        {
            var name =  v.StringValue.Trim().Split(" ")[0];

            if (!Regex.IsMatch(name, PbsInterpreter.TokenRegex))
            {
                return PbsValue.False;
            }

            var obj = _creator(name);
            
            env.RegisterObject(obj, true);
            return PbsValue.True;
        }
        
        return PbsValue.False;
    }

    protected override bool Is(string param)
    {
        return false;
    }

    public override string GetDocumentation()
    {
        return $"Creates a new Objects of the type {ObjectName} with '{ObjectName} create $varName'";
    }

    public override string ObjectName { get; }
    public override string GetStringValue()
    {
        return "null";
    }
}