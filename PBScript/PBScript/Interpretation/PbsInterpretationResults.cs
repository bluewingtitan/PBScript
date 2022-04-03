using PBScript.Interfaces;

namespace PBScript.Interpretation;

public class PbsInterpretationResults
{
    public PbsRuntime GetRuntime(IPbsEnvironment environment)
    {
        return new PbsRuntime(environment, this);
    }
    
    public List<IPbsElement>? Elements = null;

    public int TotalLines = 0;
    public int LinesOfCode = 0;
}