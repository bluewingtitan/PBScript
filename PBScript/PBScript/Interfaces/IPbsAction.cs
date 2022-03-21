namespace PBScript.Interfaces;

/// <summary>
/// Represents a specific part like "me is moved", evaluating to a boolean.
/// </summary>
public interface IPbsAction
{
    /// <summary>
    /// Executes this action.
    /// </summary>
    /// <returns>If action was successful</returns>
    public IPbsValue Execute(IPbsEnvironment env);
}