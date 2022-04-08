using PBScript.Interfaces;

namespace PBScript.Fruity.FruityVm;

public class FruityEnvironment: IEnvironment
{
    private readonly IScope _globalScope = new FruityScope(new Stack<PbsValue>());
    private readonly Stack<IScope> _scopes = new();
    private IScope CurrentScope => _scopes.Count > 0 ? _scopes.Peek() : _globalScope;
    public Stack<PbsValue> CurrentStack => CurrentScope.ValueStack;
    private readonly Dictionary<string, PbsValue> _values = new();

    /// <summary>
    /// Stack of scopes meant to recycle scopes instead of creating new ones.
    /// </summary>
    private readonly Stack<IScope> _recycleScopes = new();


    public PbsValue GetValue(string key)
    {
        if (_values.ContainsKey(key))
            return _values[key];
        
        return PbsValue.Null;
    }

    public void RegisterScoped(PbsValue value, string key)
    {
        // was defined in this or upper scope?
        if (_values.ContainsKey(key))
        {
            // is defined in this exact scope?
            if (CurrentScope.AlreadyDefinedInScope(key))
            {
                _values[key] = value;
            }
            else
            {
                // handle going out of scope
                var oldVal = _values[key];
                _values[key] = value;
                CurrentScope.OnClose += () =>
                {
                    _values[key] = oldVal;
                };
            }
        }
        else
        {
            _values[key] = value;
            CurrentScope.OnClose += () =>
            {
                _values.Remove(key);
            };
        }
    }

    public void StartScope()
    {
        // recycle old scope if possible
        var scope = _recycleScopes.TryPop(out var scopeCandidate) 
            ? scopeCandidate 
            : new FruityScope(new Stack<PbsValue>());
        
        _scopes.Push(scope);
    }

    public void StopScope()
    {
        var scope = _scopes.Pop();
        scope.Close();
        
        // prepare scope for recycling
        scope.Clear();
        _recycleScopes.Push(scope);
    }
}