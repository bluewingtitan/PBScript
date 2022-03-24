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


    private IPbsValue SaveAs(IPbsValue v, IPbsEnvironment env)
    {
        if (v.StringValue == null)
            return PbsValue.False;
        
        Results[v.StringValue] = _lastResult;
        return PbsValue.True;
    }
    
    private IPbsValue AssertNull(IPbsValue v, IPbsEnvironment env)
         {
             if (v.ReturnType is VariableType.Null)
             {
                 _lastResult = true;
                 return PbsValue.True;
             }
             
             _lastResult = false;
             return PbsValue.False;
         }

    private IPbsValue AssertNotNull(IPbsValue v, IPbsEnvironment env)
    {
        if (v.ReturnType is not VariableType.Null && 
            v.ReturnType is not VariableType.Unsupported)
        {
            _lastResult = true;
            return PbsValue.True;
        }
        
        _lastResult = false;
        return PbsValue.False;
    }
    
    private IPbsValue AssertTrue(IPbsValue v, IPbsEnvironment env)
    {
        if (v.BooleanValue is true)
        {
            _lastResult = true;
            return PbsValue.True;
        }
        
        _lastResult = false;
        return PbsValue.False;
    }
    
    private IPbsValue AssertFalse(IPbsValue v, IPbsEnvironment env)
    {
        if (v.BooleanValue is false)
        {
            _lastResult = true;
            return PbsValue.True;
        }
        
        _lastResult = false;
        return PbsValue.False;
    }
    protected override IPbsValue DefaultAction(string param)
    {
        return PbsValue.True;
    }

    public override string GetDocumentation()
    {
        return "Used to assert different things in-language. Saves the results as true/false in an dictionary. Useful for Unit-Tests.";
    }

    public override string ObjectName => "assert";
    public override string ObjectType => "assert";
    public override string GetStringValue()
    {
        return ObjectType;
    }
}