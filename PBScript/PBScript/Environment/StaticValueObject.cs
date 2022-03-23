using PBScript.Interfaces;

namespace PBScript.Environment;

public class StaticValueObject: ObjectBase
{
    public StaticValueObject(IPbsValue value, string objectName)
    {
        _value = value;
        ObjectName = objectName;
    }

    protected override IPbsValue DefaultAction(string param)
    {
        return _value;
    }

    protected override bool Is(string param)
    {
        return param.Equals(_value.AsString());
    }

    public override string GetDocumentation()
    {
        return
            "Represents a single, static value. Used for values like true, false and null inside of the PbsEnvironment.";
    }

    public override string ObjectName { get; }
    private readonly IPbsValue _value;
    public override string GetStringValue()
    {
        return _value.AsString();
    }
}