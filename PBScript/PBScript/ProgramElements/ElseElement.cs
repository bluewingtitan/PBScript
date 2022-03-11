using PBScript.Exception;
using PBScript.Interfaces;

namespace PBScript.ProgramElements;

public class ElseElement: ElementBase, IPbsBlockEnd, IPbsBlockStart
{
    public override string Token { get; protected set; } = "end";
    
    public override int Execute(IPbsEnvironment env)
    {
        if (_blockStart == null)
            return LineIndex + 1;
        
        if (!_blockStart.LastConditionMet())
            return LineIndex + 1;
            
        return BlockEndLineIndex;
    }

    public override bool CheckValid()
    {
        if (_blockStart == null)
        {
            throw new UnexpectedBlockEndException(Token, SourceCodeLineNumber);
        }
        if (!_blockStart.Token.Equals("if"))
        {
            throw new InvalidElseTokenException(SourceCodeLineNumber);
        }

        return true;
    }

    public int BlockStartLineIndex { get; private set; }
    private IPbsBlockStart? _blockStart;
    public void RegisterBlockStart(IPbsBlockStart blockStart)
    {
        if (!blockStart.Token.Equals("if"))
        {
            throw new InvalidElseTokenException(SourceCodeLineNumber);
        }

        _blockStart = blockStart;
        BlockStartLineIndex = blockStart.LineIndex;
    }

    public int BlockEndLineIndex { get; private set; }
    public void RegisterBlockEnd(IPbsBlockEnd blockEnd)
    {
        BlockEndLineIndex = blockEnd.LineIndex;
    }

    // has no condition that could have been met!
    public bool LastConditionMet() => false;
}