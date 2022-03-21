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

            return false;
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
        var s = "";
        if (StringValue != null)
        {
            s= $" \"{StringValue}\"";
        }
        else
        {
            s= " " + (ObjectValue?.ToString() ?? "false");
        }

        
        if(PbsInterpreter.Log) Console.WriteLine("got: " + s);

        return s;
    }
    
    public object? ObjectValue { get; }
}

public class PbsValue : IPbsValue
{
    public static IPbsValue Null = new PbsValue(false);
    public static IPbsValue True = new PbsValue(true, false);
    public static IPbsValue False = new PbsValue(false, false);
    
    public object? ObjectValue { get; protected set; }

    /// <summary>
    /// Whether this object's value may be changed with this.SetObjectValue()
    /// </summary>
    public bool Unlocked => true;

    private readonly bool _unlocked;

    private readonly IPbsValue _asValue;

    public PbsValue(bool unlocked)
    {
        if(PbsInterpreter.Log) Console.WriteLine("created: " + "null");
        ObjectValue = null;
        _asValue = this;
        _unlocked = unlocked;
    }
    
    public PbsValue(string objectValue, bool unlocked = false)
    {
        if(PbsInterpreter.Log) Console.WriteLine("created: s." + objectValue);
        ObjectValue = objectValue;
        _asValue = this;
        _unlocked = unlocked;
    }

    public PbsValue(double objectValue, bool unlocked = false)
    {
        if(PbsInterpreter.Log) Console.WriteLine("created: d." + objectValue);
        ObjectValue = objectValue;
        _asValue = this;
        _unlocked = unlocked;
    }
    
    public PbsValue(bool objectValue, bool unlocked = false)
    {
        if(PbsInterpreter.Log) Console.WriteLine("created: b." + objectValue);
        ObjectValue = objectValue;
        _asValue = this;
        _unlocked = unlocked;
    }
    
    public PbsValue(object? objectValue, bool unlocked = false)
    {
        if(PbsInterpreter.Log) Console.WriteLine("created: o." + (objectValue?.ToString()?? "null"));
        _asValue = this;
        _unlocked = unlocked;

        if (objectValue == null)
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

        ObjectValue = null;
    }

    public void SetObjectValue(string newValue)
    {
        if (!Unlocked)
        {
            return;
        }
        
        if(PbsInterpreter.Log) if(PbsInterpreter.Log) Console.WriteLine("set: " + newValue);
        
        ObjectValue = newValue;
    }
    
    public void SetObjectValue(double newValue)
    {
        if (!Unlocked)
        {
            return;
        }
        if(PbsInterpreter.Log) Console.WriteLine("set: " + newValue);
        ObjectValue = newValue;
    }
    
    public void SetObjectValue(bool newValue)
    {
        if (!Unlocked)
        {
            return;
        }
        if(PbsInterpreter.Log) Console.WriteLine("set: " + newValue);
        ObjectValue = newValue;
    }
    
    // map class-api to interface-defaults
    public VariableType ReturnType => _asValue.ReturnType;
    public bool? BooleanValue => _asValue.BooleanValue;
    public double? NumberValue => _asValue.NumberValue;
    public string? StringValue => _asValue.StringValue;
    
    public override string ToString()
    {
        return _asValue.AsString();
    }
}