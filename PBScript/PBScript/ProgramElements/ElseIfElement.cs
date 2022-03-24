using PBScript.Environment;
using PBScript.Exception;
using PBScript.Interfaces;

namespace PBScript.ProgramElements;

public class ElseIfElement: ConditionalBlockStart, IPbsBlockEnd
{
    public override string Token { get; } = "elseif";
    
    public override int Execute(IPbsEnvironment env)
    {
        if (_blockStart == null)
            throw new UnclosedBlockException(Token, SourceCodeLineNumber);

        if (_blockStart.LastConditionMet())
        {
            // passes the true down the elseif/else-chain.
            LastResult = true;
            return BlockEndLineIndex;
        }

        var r = _metaAction?.Execute(env);
        LastResult = r is {ReturnType: VariableType.Boolean, BooleanValue: { }} && (bool) r.BooleanValue;

        if (LastResult)
            return LineIndex + 1;

        return BlockEndLineIndex;
    }
    
    public override void ThrowIfNotValid()
    {
        if (_blockStart == null)
        {
            throw new UnexpectedBlockEndException(Token, SourceCodeLineNumber);
        }
        
        base.ThrowIfNotValid();
    }
    
    private IPbsBlockStart? _blockStart;
    public void RegisterBlockStart(IPbsBlockStart blockStart)
    {
        if (!blockStart.Token.Equals("if") && !blockStart.Token.Equals("elseif"))
        {
            throw new InvalidElseTokenException(SourceCodeLineNumber);
        }

        _blockStart = blockStart;
    }
}