using PBScript.Interfaces;

namespace PBScript.Environment.Random;

/// <summary>
/// To create some randomness, this one will create boolean randomness.
///
/// Usage:
/// request with 'request random'
/// use like
/// if random
///     // code goes here...
/// end
/// </summary>
public class RandomObject: ObjectBase
{
    public override string ObjectName => "random";
    private readonly System.Random _r = new System.Random();
    protected override bool Is(string param)
    {
        return false;
    }

    public RandomObject()
    {
        Register("bool", (s, e) => new PbsValue(NextBoolean()));
        Register("boolean", (s, e) => new PbsValue(NextBoolean()));
        Register("number", (s, e) => new PbsValue(NextNumber()));
    }

    protected override IPbsValue DefaultAction(string param)
    {
        return new PbsValue(NextBoolean());
    }

    private bool NextBoolean()
    {
        return _r.Next(0, 2) == 1;
    }
    
    private double NextNumber()
    {
        return _r.Next(0, 100);
    }

    public override string GetDocumentation()
    {
        return "Any usage in if-statements will result in a random boolean, Usage as $random will result in a random integer between 0 (inclusive) and 100 (exclusive)";
    }

    public override string GetStringValue()
    {
        return NextNumber() + "";
    }
}