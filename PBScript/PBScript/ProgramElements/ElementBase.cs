using PBScript.Interfaces;

namespace PBScript.ProgramElements;

public abstract class ElementBase: IPbsElement
{
    public abstract string Token { get; protected set; }
    public string LineText { get; private set; } = "";
    public int LineIndex { get; private set; }
    public int SourceCodeLineNumber { get; private set; }
    
    public abstract int Execute(IPbsEnvironment env);

    public abstract bool CheckValid();

    public virtual void ParseLine(string code, int lineIndex, int sourceCodeLineNumber)
    {
        LineText = code;
        LineIndex = lineIndex;
        SourceCodeLineNumber = sourceCodeLineNumber;
    }
}