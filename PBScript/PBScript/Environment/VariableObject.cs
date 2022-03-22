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
        Register("set", AssignValue);
        Register("=", AssignValue);
        Register("add", AddValue);
        Register("+=", AddValue);
        Register("remove", RemoveValue);
        Register("-=", RemoveValue);
        Register("rmv", RemoveValue);
        Register("divide", DivideValue);
        Register("/=", DivideValue);
        Register("div", DivideValue);
        Register("multiply", DivideValue);
        Register("mtp", MultiplyValue);
        Register("*=", MultiplyValue);
        Register("ply", MultiplyValue);
        Register("up", PlusPlusValue);
        Register("++", PlusPlusValue);
        Register("down", MinusMinusValue);
        Register("--", MinusMinusValue);
    }

    #region Actions

    private IPbsValue AssignValue(string args, IPbsEnvironment env)
    {
        Value = new PbsAction($"({args})").Execute(env);
        return PbsValue.True;
    }
    private IPbsValue AddValue(string args, IPbsEnvironment env)
    {
        var addV = new PbsAction($"({args})").Execute(env);

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
    
    private IPbsValue RemoveValue(string args, IPbsEnvironment env)
    {
        var rV = new PbsAction($"({args})").Execute(env);

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
    
    private IPbsValue DivideValue(string args, IPbsEnvironment env)
    {
        var dV = new PbsAction($"({args})").Execute(env);

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
    
    private IPbsValue MultiplyValue(string args, IPbsEnvironment env)
    {
        var dV = new PbsAction($"({args})").Execute(env);

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