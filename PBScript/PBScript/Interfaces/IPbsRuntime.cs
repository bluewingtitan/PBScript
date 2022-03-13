namespace PBScript.Interfaces;

public interface IPbsRuntime
{
    public bool IsFinished { get; }
    public void ExecuteNext();
    public void ExecuteAll();
}