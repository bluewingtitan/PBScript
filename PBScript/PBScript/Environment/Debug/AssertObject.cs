using PBScript.Interfaces;

namespace PBScript.Environment.Debug;

public class AssertObject: ObjectBase
{
    public Dictionary<string, bool> Results { get; } = new();

    private bool _lastResult = false;

    public AssertObject()
    {
        RegisterTyped("save", SaveAs);
        RegisterTyped("true", AssertTrue);
        RegisterTyped("false", AssertFalse);
        RegisterTyped("null", AssertNull);
        RegisterTyped("notnull", AssertNotNull);
    }


    private PbsValue SaveAs(PbsValue[] value, IPbsEnvironment env)
    {
        if (value.Length < 1)
        {
            return PbsValue.False;
        }

        var v = value[0];
        
        if (v.StringValue == null)
            return PbsValue.False;
        
        Results[v.StringValue] = _lastResult;
        return PbsValue.True;
    }
    
    private PbsValue AssertNull(PbsValue[] value, IPbsEnvironment env)
    {
        if (value.Length < 1)
        {
            return PbsValue.False;
        }

        var v = value[0];

        if (v.ReturnType is VariableType.Null)
        {
            _lastResult = true;
            return PbsValue.True;
        }
             
        _lastResult = false;
        return PbsValue.False;
    }

    private PbsValue AssertNotNull(PbsValue[] value, IPbsEnvironment env)
    {
        if (value.Length < 1)
        {
            return PbsValue.False;
        }

        var v = value[0];
        
        if (v.ReturnType is not VariableType.Null && 
            v.ReturnType is not VariableType.Undefined)
        {
            _lastResult = true;
            return PbsValue.True;
        }
        
        _lastResult = false;
        return PbsValue.False;
    }
    
    private PbsValue AssertTrue(PbsValue[] value, IPbsEnvironment env)
    {
        if (value.Length < 1)
            return PbsValue.False;
        
        var v = value[0];
        
        
        if (v.BooleanValue is true)
        {
            _lastResult = true;
            return PbsValue.True;
        }
        
        _lastResult = false;
        return PbsValue.False;
    }
    
    private PbsValue AssertFalse(PbsValue[] value, IPbsEnvironment env)
    {
        if (value.Length < 1)
            return PbsValue.False;
        
        var v = value[0];

        if (v.BooleanValue is false)
        {
            _lastResult = true;
            return PbsValue.True;
        }
        
        _lastResult = false;
        return PbsValue.False;
    }
    protected override PbsValue DefaultAction(PbsValue[] param)
    {
        return PbsValue.True;
    }

    public override string GetDocumentation()
    {
        return "Used to assert different things in-language. Saves the results as true/false in an dictionary. Useful for Unit-Tests.";
    }

    public override string ObjectName => "assert";
}