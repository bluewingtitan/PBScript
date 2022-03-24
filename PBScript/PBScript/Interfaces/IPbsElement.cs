namespace PBScript.Interfaces;

public interface IPbsElement
{
    public string Token { get; }
    public string LineText { get; }
    public int LineIndex { get; }
    public int SourceCodeLineNumber { get; }
    
    /// <summary>
    /// Executes the next element
    /// </summary>
    /// <returns>The next line to execute</returns>
    public int Execute(IPbsEnvironment env);

    /// <summary>
    /// Called after all code was processed, as a last checking instance that every element has all information needed and was properly converted.
    /// May throw errors corresponding to missing things. If just returning false, an InvalidLineException will be thrown.
    /// </summary>
    /// <returns>Valid state (true if valid)</returns>
    public void ThrowIfNotValid();

    /// <summary>
    /// Reads the line into the object
    /// </summary>
    /// <returns>The left over string not contained in this element</returns>
    public void ParseLine(string code, int lineIndex, int sourceCodeLineNumber);
}

public interface IPbsBlockStart: IPbsElement
{
    public void RegisterBlockEnd(IPbsBlockEnd blockEnd);

    // 
    public bool LastConditionMet();
}

public interface IPbsBlockEnd: IPbsElement
{
    public void RegisterBlockStart(IPbsBlockStart blockStart);
}