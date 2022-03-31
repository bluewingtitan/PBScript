using PBScript.Interfaces;

namespace PBScript.Environment.Collections;
// => A key value store.
public class DictionaryObject: ObjectBase
{
    private Dictionary<object, PbsValue> _dict = new();
    private bool _isReadOnly = false;

    public DictionaryObject(string objectName, bool isReadOnly = false)
    {
        ObjectName = objectName;
        _isReadOnly = false;
        RegisterActions();
    }

    public DictionaryObject(string objectName, Dictionary<object, PbsValue> dict, bool isReadOnly = false)
    {
        ObjectName = objectName;
        _dict = dict;
        _isReadOnly = false;
        RegisterActions();
    }

    private void RegisterActions()
    {
        Register("get", Get);
        Register("set", Set);
        Register("remove", Delete);
        Register("delete", Delete);
        Register("count", Count);
    }

    private PbsValue Count(PbsValue[] param, IPbsEnvironment env) => new (_dict.Count);
    private PbsValue Get(PbsValue[] param, IPbsEnvironment env)
    {
        if (param.Length < 1)
            return PbsValue.Null;

        var key = param[0].ObjectValue;
        if (key == null)
            return PbsValue.Null;

        if (_dict.TryGetValue(key, out var val))
            return val;
        
        return PbsValue.Null;
    }

    private PbsValue Delete(PbsValue[] param, IPbsEnvironment env)
    {
        if (_isReadOnly)
            return PbsValue.False;
        if (param.Length < 1)
            return PbsValue.False;

        var key = param[0].ObjectValue;
        if (key == null)
            return PbsValue.False;

        if (_dict.ContainsKey(key) && _dict.Remove(key))
            return PbsValue.True;

        return PbsValue.False;
    }

    private PbsValue Set(PbsValue[] param, IPbsEnvironment env)
    {
        if (_isReadOnly)
            return PbsValue.False;
        if (param.Length < 2)
            return PbsValue.False;

        var key = param[0].ObjectValue;
        if (key == null)
            return PbsValue.False;

        _dict[key] = param[1];
        
        return PbsValue.True;
    }

    protected override PbsValue DefaultAction(string command, PbsValue[] param, IPbsEnvironment env)
    {
        return PbsValue.Null;
    }

    public override string GetDocumentation()
    {
        return "";
    }

    public override string ObjectName { get; }
}