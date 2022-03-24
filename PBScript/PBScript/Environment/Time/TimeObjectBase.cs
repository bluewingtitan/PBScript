using PBScript.Interfaces;

namespace PBScript.Environment.Time;

public abstract class TimeObjectBase: ObjectBase
{
    
    public override string ObjectName { get; }
    public override string ObjectType => ObjectName;
    protected abstract int GetValueFromDateTime(DateTime dt);

    private int Value => GetValueFromDateTime(_useUtc ? DateTime.UtcNow : DateTime.Now);

    private readonly bool _useUtc;

    protected TimeObjectBase(bool useUtc, string objectName)
    {
        _useUtc = useUtc;
        ObjectName = objectName;
    }

    public override string GetDocumentation()
    {
        return "Represents a numerical value the current date.";
    }

    protected override IPbsValue DefaultAction(string param)
    {
        return new PbsValue(Value);
    }

    public override string GetStringValue()
    {
        return Value.ToString();
    }
}