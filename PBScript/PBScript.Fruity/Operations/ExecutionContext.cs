namespace PBScript.Fruity.Operations;

public class ExecutionContext
{
    /// <summary>
    /// Signals that the flow was altered into another direction.
    /// </summary>
    public bool ChangeFlow { get; set; } = false;

    /// <summary>
    /// Signals to what label the runtime should jump to next.
    /// Has no effect if ChangeFlow is not set to true.
    /// </summary>
    public string NextLabel { get; set; } = "";
}