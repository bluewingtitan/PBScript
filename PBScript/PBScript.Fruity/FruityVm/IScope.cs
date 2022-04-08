using PBScript.Interfaces;

namespace PBScript.Fruity.FruityVm;

public interface IScope
{
    public delegate void OnScopeCloseDelegate();
    public event OnScopeCloseDelegate OnClose;
    public Stack<PbsValue> ValueStack { get; }

    public bool AlreadyDefinedInScope(string key);
    public void DefineInScope(string key);

    public void Close();

    /// <summary>
    /// Clears the scope to allow reuse (for better memory efficiency)
    /// </summary>
    public void Clear();
}