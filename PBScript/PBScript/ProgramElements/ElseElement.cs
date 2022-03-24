using PBScript.Exception;
using PBScript.Interfaces;

namespace PBScript.ProgramElements;

public class ElseElement: ElementBase, IPbsBlockEnd, IPbsBlockStart
{
    public override string Token { get; } = "end";
    
    public override int Execute(IPbsEnvironment env)
    {
        if (_blockStart == null)
            throw new UnclosedBlockException(Token, SourceCodeLineNumber);
        
        if (!_blockStart.LastConditionMet())
            return LineIndex + 1;
            
        return BlockEndLineIndex;
    }

    public override void ThrowIfNotValid()
    {
        if (_blockStart == null)
        {
            throw new UnexpectedBlockEndException(Token, SourceCodeLineNumber);
        }
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

    public int BlockEndLineIndex { get; private set; }
    public void RegisterBlockEnd(IPbsBlockEnd blockEnd)
    {
        BlockEndLineIndex = blockEnd.LineIndex;
    }

    // has no condition that could have been met!
    public bool LastConditionMet() => false;
}