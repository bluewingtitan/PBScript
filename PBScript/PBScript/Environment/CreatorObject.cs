using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;
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

    private PbsValue Create(PbsValue[] v, IPbsEnvironment env)
    {
        if (v.Length<1)
        {
            return PbsValue.False;
        }
        
        var name = v[0].StringValue ?? "";

        if (!Regex.IsMatch(name, PbsInterpreter.TokenRegex))
        {
            return PbsValue.False;
        }

        var obj = _creator(name);
            
        env.RegisterObject(obj);
        return PbsValue.True;
    }

    protected override PbsValue DefaultAction(PbsValue[] param)
    {
        return PbsValue.Null;
    }

    public override string GetDocumentation()
    {
        return $"Creates a new Objects of the type {ObjectName} with '{ObjectName} create $varName'";
    }

    public override string ObjectName { get; }
}