using PBScript.Interfaces;

namespace PBScript.Interpretation;

public struct PbsInterpretationResults
{
    public PbsRuntime GetRuntime(IPbsEnvironment environment)
    {
        return new PbsRuntime(environment, this);
    }
    
    public List<IPbsElement> Elements;

    public int TotalLines;
    public int LinesOfCode;
}