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

    public override string GetDocumentation()
    {
        return
            "Represents a single, static value. Used for values like true, false and null inside of the PbsEnvironment.";
    }

    public override string ObjectName { get; }
    public override string ObjectType => _value.ReturnType.ToString();
    private readonly IPbsValue _value;
    public override string GetStringValue()
    {
        return _value.AsString();
    }
}