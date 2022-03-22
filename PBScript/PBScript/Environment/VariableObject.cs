using PBScript.Interfaces;
using PBScript.ProgramElements;

namespace PBScript.Environment;

public class VariableObject : ObjectBase
{
    public IPbsValue Value { get; private set; }
    public VariableType ValueType => Value.ReturnType;

    public VariableObject(string objectName)
    {
        ObjectName = objectName;
        Value = new PbsValue((object?) null);

        // TODO: Register all methods!
        RegisterTyped("set", AssignValue);
        RegisterTyped("=", AssignValue);
        RegisterTyped("add", AddValue);
        RegisterTyped("+=", AddValue);
        RegisterTyped("remove", RemoveValue);
        RegisterTyped("-=", RemoveValue);
        RegisterTyped("rmv", RemoveValue);
        RegisterTyped("divide", DivideValue);
        RegisterTyped("/=", DivideValue);
        RegisterTyped("div", DivideValue);
        RegisterTyped("multiply", DivideValue);
        RegisterTyped("mtp", MultiplyValue);
        RegisterTyped("*=", MultiplyValue);
        RegisterTyped("ply", MultiplyValue);
        Register("up", PlusPlusValue);
        Register("++", PlusPlusValue);
        Register("down", MinusMinusValue);
        Register("--", MinusMinusValue);
    }

    #region Actions

    private IPbsValue AssignValue(IPbsValue v, IPbsEnvironment env)
    {
        Value = v;
        return PbsValue.True;
    }
    private IPbsValue AddValue(IPbsValue addV, IPbsEnvironment env)
    {
        if (addV.ReturnType != ValueType && ValueType != VariableType.Null && ValueType != VariableType.Unsupported)
            return PbsValue.False;

        switch (addV.ReturnType)
        {
            case VariableType.Number:
                var v = Value.NumberValue ?? 0;
                var v2 = addV.NumberValue ?? 0;

                Value.SetObjectValue(v + v2);
                break;
            
            case VariableType.String:
                var s = Value.StringValue ?? "";
                var s2 = addV.StringValue ?? "";

                Value.SetObjectValue(s + s2);
                break;

            default:
                return PbsValue.False;
        }
        return PbsValue.True;
    }
    
    private IPbsValue RemoveValue(IPbsValue rV, IPbsEnvironment env)
    {
        if (rV.ReturnType != ValueType && ValueType != VariableType.Null && ValueType != VariableType.Unsupported)
            return PbsValue.False;

        switch (rV.ReturnType)
        {
            case VariableType.Number:
                var v = Value.NumberValue ?? 0;
                var v2 = rV.NumberValue ?? 0;

                Value.SetObjectValue(v - v2);
                break;
            default:
                return PbsValue.False;
        }
        return PbsValue.True;
    }
    
    private IPbsValue DivideValue(IPbsValue dV, IPbsEnvironment env)
    {
        if (dV.ReturnType != ValueType && ValueType != VariableType.Null && ValueType != VariableType.Unsupported)
            return PbsValue.False;

        switch (dV.ReturnType)
        {
            case VariableType.Number:
                var v = Value.NumberValue ?? 0;
                var v2 = dV.NumberValue ?? 0.000000001d;
                
                if (v2==0)
                {
                    v2 = 0.000000001d;
                }
                
                Value.SetObjectValue(v / v2);
                break;
            default:
                return PbsValue.False;
        }
        return PbsValue.True;
    }
    
    private IPbsValue MultiplyValue(IPbsValue dV, IPbsEnvironment env)
    {
        if (dV.ReturnType != ValueType && ValueType != VariableType.Null && ValueType != VariableType.Unsupported)
            return PbsValue.False;

        switch (dV.ReturnType)
        {
            case VariableType.Number:
                var v = Value.NumberValue ?? 0;
                var v2 = dV.NumberValue ?? 0;
                
                Value.SetObjectValue(v * v2);
                break;
            default:
                return PbsValue.False;
        }
        return PbsValue.True;
    }

    private IPbsValue PlusPlusValue(string args, IPbsEnvironment env)
    {
        if (ValueType is VariableType.String or VariableType.Boolean)
            return PbsValue.False;

        var v = Value.NumberValue??0;
        Value.SetObjectValue(v+1);
        
        return PbsValue.True;
    }
    private IPbsValue MinusMinusValue(string args, IPbsEnvironment env)
    {
        if (ValueType is VariableType.String or VariableType.Boolean)
            return PbsValue.False;

        var v = Value.NumberValue??0;
        Value.SetObjectValue(v-1);
        
        return PbsValue.True;
    }
        
    #endregion

    public override string GetDocumentation()
    {
        return "The default holder of different values.";
    }

    public override string ObjectName { get; }

    public override string GetStringValue()
    {
        return Value.AsString();
    }

    protected override IPbsValue DefaultAction(string param)
    {
        return new PbsValue(Value.ObjectValue);
    }
    
    protected override bool Is(string param)
    {
        return ValueType.ToString().ToLower().Trim()
            .Equals(param.ToLower().Trim());
    }
}

public enum VariableType
{
    Boolean,
    Number,
    String,
    Unsupported,
    Null,
}