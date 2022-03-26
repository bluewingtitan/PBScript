using PBScript.Interfaces;

namespace PBScript.Environment;

public class StaticValueObject: ObjectBase
{
    public StaticValueObject(PbsValue value, string objectName)
    {
        _value = value;
        ObjectName = objectName;
    }

    protected override PbsValue DefaultAction(PbsValue[] param)
    {
        return _value;
    }

    public override string GetDocumentation()
    {
        return
            "Represents a single, static value. Used for values like true, false and null inside of the PbsEnvironment.";
    }

    public override string ObjectName { get; }
    private readonly PbsValue _value;
}