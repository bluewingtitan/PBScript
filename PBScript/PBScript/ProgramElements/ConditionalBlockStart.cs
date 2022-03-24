using PBScript.Environment;
using PBScript.Exception;
using PBScript.Interfaces;

namespace PBScript.ProgramElements;

public abstract class ConditionalBlockStart: ElementBase, IPbsBlockStart
{
    protected IPbsAction? _metaAction;
    
    public override int Execute(IPbsEnvironment env)
    {
        if (_metaAction == null)
        {
            throw new NotProperlyInitializedException(Token);
        }
        
        var r = _metaAction?.Execute(env);
        LastResult = r is {ReturnType: VariableType.Boolean, BooleanValue: { }} && (bool) r.BooleanValue;

        if (LastResult)
            return LineIndex + 1;

        return BlockEndLineIndex;
    }
    
    public override void ThrowIfNotValid()
    {
        if (_metaAction == null)
        {
            throw new NotProperlyInitializedException(Token);
        }
        
        if (LineText.Trim().Equals(Token))
        {
            throw new InvalidConditionException(LineText, SourceCodeLineNumber);
        }
    }
    
    public override void ParseLine(string code, int lineIndex, int sourceCodeLineNumber)
    {
        base.ParseLine(code, lineIndex, sourceCodeLineNumber);
        var actionCode = "";
        
        actionCode = code.Split(Token,2)[1].Trim();
        
        _metaAction = new PbsAction("(" + actionCode + ")");
    }

    public int BlockEndLineIndex { get; private set; }
    public void RegisterBlockEnd(IPbsBlockEnd blockEnd)
    {
        BlockEndLineIndex = blockEnd.LineIndex;
    }

    protected bool LastResult;

    public bool LastConditionMet()
    {
        return LastResult;
    }
}