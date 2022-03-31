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

        Register("create", Create);
    }

    private PbsValue Create(PbsValue[] v, IPbsEnvironment env)
    {
        return new PbsValue(_creator("wrapped_object"));
    }

    protected override PbsValue DefaultAction(string command, PbsValue[] param, IPbsEnvironment env)
    {
        return PbsValue.Null;
    }

    public override string GetDocumentation()
    {
        return $"Creates a new Objects of the type {ObjectName} with '{ObjectName} create $varName'";
    }

    public override string ObjectName { get; }
}