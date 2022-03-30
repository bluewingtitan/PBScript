using PBScript.Environment;
using PBScript.ExpressionParsing;
using PBScript.ExpressionParsing.Operators;
using PBScript.Extension;
using PBScript.Interpretation;

namespace PBScript.Interfaces;

public class PbsValue: IExpressionValue
{
    public static int MaxStringLength = 256;
    
    public VariableType ReturnType
    {
        get
        {
            if (ObjectValue == null) return VariableType.Null;
            if (NumberValue != null) return VariableType.Number;
            if (StringValue != null) return VariableType.String;
            if (BooleanValue != null) return VariableType.Boolean;
            if (ObjectValue is IExpressionParenthesisOperator) return VariableType.Token_FunctionClosure;
            if (PbsObjectValue != null) return VariableType.PbsObject;

            // => Not null, bot none of the supported types.
            return VariableType.Undefined;
        }
    }

    public IPbsObject? PbsObjectValue
    {
        get
        {
            if (ObjectValue is IPbsObject po)
            {
                return po;
            }

            return null;
        }
    }

    public bool? BooleanValue
    {
        get
        {
            if (ObjectValue is bool b)
            {
                return b;
            }

            return null;
        }
    }

    public double? NumberValue
    {
        get
        {
            if (ObjectValue is double d)
            {
                return d;
            }

            return null;
        }
    }

    public string? StringValue
    {
        get
        {
            if (ObjectValue is string s)
            {
                return s;
            }

            return null;
        }
    }

    public string AsString()
    {
        var s = StringValue != null
            ?$"\"{StringValue}\""
            :(ObjectValue?.ToString()?.ToLower() ?? "null");

        return s;
    }
    
    
    
    public static PbsValue Null = new PbsValue().Lock();
    public static PbsValue True = new PbsValue(true).Lock();
    public static PbsValue False = new PbsValue(false).Lock();

    public object? ObjectValue { get; private set; }

    public bool IsLocked { get; private set; } = false;

    public PbsValue Lock()
    {
        IsLocked = true;
        return this;
    }

    public PbsValue()
    {
        //if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] created: <" + "null" + ">");
        ObjectValue = null;
    }
    
    public PbsValue(string objectValue)
    {
        //if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] created: string <" + (objectValue ?? "null") + ">");
        ObjectValue = objectValue.Length>MaxStringLength? objectValue.Substring(0, MaxStringLength): objectValue;
    }

    public PbsValue(double objectValue)
    {
        //if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] created: number <" + (objectValue.ToString()?? "null") + ">");
        ObjectValue = objectValue;
    }
    
    public PbsValue(bool objectValue)
    {
        //if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] created: bool <" + (objectValue.ToString()?? "null") + ">");
        ObjectValue = objectValue;
    }

    public PbsValue(IPbsObject objectValue)
    {
        ObjectValue = objectValue;
    }
    
    public PbsValue(object? objectValue)
    {
        //if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] created: object <" + (objectValue?.ToString()?? "null") + ">");
        SetObjectValue(objectValue, true);
    }

    public void SetObjectValue(string newValue)
    {
        if (IsLocked)
            return;
        
        if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] set value <" + (newValue?.ToString()?? "null") + ">");

        if (newValue==null)
        {
            ObjectValue = null;
            return;
        }
        
        ObjectValue = newValue.Length>MaxStringLength? newValue.Substring(0, MaxStringLength): newValue;
    }
    
    public void SetObjectValue(double newValue)
    {
        if (IsLocked)
            return;

        if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] set value <" + (newValue.ToString()?? "null") + ">");
        ObjectValue = newValue;
    }
    
    public void SetObjectValue(bool newValue)
    {
        if (IsLocked)
            return;

        if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] set value <" + (newValue.ToString()?? "null") + ">");
        ObjectValue = newValue;
    }

    public void SetObjectValue(object? objectValue) => SetObjectValue(objectValue, false);
    
    public void SetObjectValue(object? objectValue, bool suppressLog)
    {
        if (IsLocked)
            return;
        
        if(PbsInterpreter.Log && !suppressLog) Console.WriteLine("[IPbsValue] set: value <" + (objectValue?.ToString()?? "null") + ">");

        if (objectValue is null)
        {
            ObjectValue = null;
            return;
        }

        if (objectValue.IsNumber())
        {
            ObjectValue = Convert.ToDouble(objectValue);
            return;
        }

        if (objectValue is string s)
        {
            ObjectValue = s.Length>MaxStringLength? s.Substring(0, MaxStringLength): s;
            return;
        }

        ObjectValue = objectValue;
    }

    // IExpressionElement Implementation.
    public bool IsConsistent => IsLocked;
    
    public PbsValue GetValue()
    {
        return this;
    }

    public override string ToString()
    {
        return AsString();
    }
}