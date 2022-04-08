using System.ComponentModel;
using PBScript.Interfaces;

namespace PBScript.Fruity.FruityVm;

public class FruityScope: IScope
{
    public FruityScope(Stack<PbsValue> valueStack)
    {
        ValueStack = valueStack;
    }

    public event IScope.OnScopeCloseDelegate? OnClose;
    public Stack<PbsValue> ValueStack { get; }

    private readonly List<string> _valueKeys = new();
    
    public bool AlreadyDefinedInScope(string key)
    {
        return _valueKeys.Contains(key);
    }

    public void DefineInScope(string key)
    {
        if (!_valueKeys.Contains(key))
            _valueKeys.Add(key);
    }

    public void Close()
    {
        OnClose?.Invoke();
    }

    public void Clear()
    {
        ValueStack.Clear();
        _valueKeys.Clear();
        // reset event
        OnClose = null;
    }
}