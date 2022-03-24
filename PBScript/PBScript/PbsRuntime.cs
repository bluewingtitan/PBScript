using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript;

public class PbsRuntime
{
    public bool IsFinished => _pointer >= _elements.Count;

    private int _pointer = 0;
    private readonly List<IPbsElement> _elements;
    private readonly IPbsEnvironment _environment;

    public PbsRuntime(IPbsEnvironment environment, PbsInterpretationResults results)
    {
        _environment = environment;
        _elements = results.Elements ?? new List<IPbsElement>();
    }


    public void ExecuteNext()
    {
        if(!IsFinished)
        {

            if (PbsInterpreter.Log)
            {
                _environment.Log("runtime", "RUN #" + _pointer);
            }

            _pointer = _elements[_pointer].Execute(_environment);
        }
    }

    public void ExecuteAll()
    {
        while (!IsFinished)
        {
            ExecuteNext();
        }
    }
}