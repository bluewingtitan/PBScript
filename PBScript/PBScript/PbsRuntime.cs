using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript;

public class PbsRuntime: IPbsRuntime
{
    public bool IsFinished => _pointer >= _elements.Count;
    private int _pointer = 0;
    private readonly List<IPbsElement> _elements;
    private readonly IPbsEnvironment _environment;

    public PbsRuntime(IPbsEnvironment environment, PbsInterpretationResults results)
    {
        _environment = environment;
        _elements = results.Elements;
    }

    public void ExecuteNext()
    {
        if(IsFinished)
            return;
        
        _pointer = _elements[_pointer].Execute(_environment);
    }

    public void ExecuteAll()
    {
        while (!IsFinished)
        {
            ExecuteNext();
        }
    }
}