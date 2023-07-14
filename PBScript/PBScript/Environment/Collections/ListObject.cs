using PBScript.Interfaces;

namespace PBScript.Environment.Collections;

public class ListObject: ObjectBase
{
    private List<PbsValue> _list = new();
    private bool _isReadOnly = false;

    public ListObject(string objectName, bool isReadOnly = false)
    {
        ObjectName = objectName;
        _isReadOnly = isReadOnly;
        RegisterActions();
    }

    public ListObject(string objectName, List<PbsValue> startList, bool isReadOnly = false)
    {
        _list = startList;
        ObjectName = objectName;
        _isReadOnly = isReadOnly;
        RegisterActions();
    }

    private void RegisterActions()
    {
        Register("get", Get);
        Register("add", Add);
        Register("set", Set);
        Register("remove", Delete);
        Register("delete", Delete);
        Register("count", Count);
    }

    private PbsValue Count(PbsValue[] param, IPbsEnvironment env) => new (_list.Count);

    private PbsValue Get(PbsValue[] param, IPbsEnvironment env)
    {
        if (param.Length < 1)
            return PbsValue.Null;

        var key = (int) Math.Round(param[0].NumberValue ?? -1);
        if (key < 0)
            return PbsValue.Null;

        if (key > _list.Count-1)
        {
            return PbsValue.Null;
        }

        return _list[key];
    }

    private PbsValue Delete(PbsValue[] param, IPbsEnvironment env)
    {
        if (_isReadOnly)
            return PbsValue.False;
        
        
        if (param.Length < 1)
            return PbsValue.False;

        var key = (int) Math.Round(param[0].NumberValue ?? -1);
        if (key < 0)
            return PbsValue.False;

        if (key < _list.Count-1)
        {
            return _list.Remove(_list[key]) ? PbsValue.True : PbsValue.False;
        }

        return PbsValue.False;
    }
    
    private PbsValue Add(PbsValue[] param, IPbsEnvironment env)
    {
        if (_isReadOnly)
            return PbsValue.False;
        
        if (param.Length < 1)
            return PbsValue.False;
        
        _list.Add(param[0]);
        
        return PbsValue.True;
    }

    private PbsValue Set(PbsValue[] param, IPbsEnvironment env)
    {
        if (_isReadOnly)
            return PbsValue.False;
        
        if (param.Length < 2)
            return PbsValue.False;
        
        var key = (int) Math.Round(param[0].NumberValue ?? -1);
        if (key < 0)
            return PbsValue.False;

        _list[key] = param[1];
        
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