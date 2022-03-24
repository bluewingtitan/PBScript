using PBScript.Environment;
using PBScript.Extension;
using PBScript.Interpretation;

namespace PBScript.Interfaces;

public interface IPbsValue
{
    public sealed VariableType ReturnType
    {
        get
        {
            if (ObjectValue == null) return VariableType.Null;
            if (NumberValue != null) return VariableType.Number;
            if (StringValue != null) return VariableType.String;
            if (BooleanValue != null) return VariableType.Boolean;

            // => Not null, bot none of the supported types.
            return VariableType.Unsupported;
        }
    }


    public void SetObjectValue(string newValue);
    public void SetObjectValue(double newValue);
    public void SetObjectValue(bool newValue);

    public sealed bool? BooleanValue
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

    public sealed double? NumberValue
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

    public sealed string? StringValue
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

    public virtual string AsString()
    {
        var s = StringValue != null
            ?$"\"{StringValue}\""
            :(ObjectValue?.ToString()?.ToLower() ?? "null");
       
        if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] got: string <" + s + ">");

        return s;
    }
    
    public object? ObjectValue { get; }
}

public class PbsValue : IPbsValue
{
    public static IPbsValue Null = new PbsValue().Lock();
    public static IPbsValue True = new PbsValue(true).Lock();
    public static IPbsValue False = new PbsValue(false).Lock();
    
    public object? ObjectValue { get; protected set; }
    private bool _isLocked = false;

    public IPbsValue Lock()
    {
        _isLocked = true;
        return this;
    }


    private readonly IPbsValue _asValue;

    public PbsValue()
    {
        if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] created: <" + "null" + ">");
        ObjectValue = null;
        _asValue = this;
    }
    
    public PbsValue(string objectValue)
    {
        if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] created: string <" + (objectValue ?? "null") + ">");
        ObjectValue = objectValue;
        _asValue = this;
    }

    public PbsValue(double objectValue)
    {
        if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] created: number <" + (objectValue.ToString()?? "null") + ">");
        ObjectValue = objectValue;
        _asValue = this;
    }
    
    public PbsValue(bool objectValue)
    {
        if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] created: bool <" + (objectValue.ToString()?? "null") + ">");
        ObjectValue = objectValue;
        _asValue = this;
    }
    
    public PbsValue(object? objectValue)
    {
        if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] created: object <" + (objectValue?.ToString()?? "null") + ">");
        _asValue = this;

        if (objectValue is null or "null")
        {
            ObjectValue = null;
            return;
        }
        
        if (objectValue is string s)
        {
            ObjectValue = s;
            return;
        }

        if (objectValue is bool b)
        {
            ObjectValue = b;
            return;
        }

        if (objectValue.IsNumber())
        {
            ObjectValue = Convert.ToDouble(objectValue);
            return;
        }

        ObjectValue = objectValue;
    }

    public void SetObjectValue(string newValue)
    {
        if (_isLocked)
            return;
        
        if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] set value <" + (newValue?.ToString()?? "null") + ">");
        
        ObjectValue = newValue;
    }
    
    public void SetObjectValue(double newValue)
    {
        if (_isLocked)
            return;

        if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] set value <" + (newValue.ToString()?? "null") + ">");
        ObjectValue = newValue;
    }
    
    public void SetObjectValue(bool newValue)
    {
        if (_isLocked)
            return;

        if(PbsInterpreter.Log) Console.WriteLine("[IPbsValue] set value <" + (newValue.ToString()?? "null") + ">");
        ObjectValue = newValue;
    }
    
    // map class-api to interface-defaults
    public VariableType ReturnType => _asValue.ReturnType;
}