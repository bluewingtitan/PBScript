using PBScript.Exception;
using PBScript.Interfaces;

namespace PBScript.ProgramElements;

public class ElseIfElement: ConditionalBlockStart, IPbsBlockEnd
{
    public override string Token { get; protected set; } = "elseif";
    
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
        
        LastResult = _metaAction.Execute(env);

        if (LastResult)
            return LineIndex + 1;

        return BlockEndLineIndex;
    }
    
    public override bool CheckValid()
    {
        if (!base.CheckValid())
            return false;
        
        if (_blockStart == null)
        {
            throw new UnexpectedBlockEndException(Token, SourceCodeLineNumber);
        }
        
        if (!_blockStart.Token.Equals("if") && !_blockStart.Token.Equals("elseif"))
        {
            throw new InvalidElseTokenException(SourceCodeLineNumber);
        }

        return true;
    }
    
    public int BlockStartLineIndex { get; private set; }
    private IPbsBlockStart? _blockStart;
    public void RegisterBlockStart(IPbsBlockStart blockStart)
    {
        if (!blockStart.Token.Equals("if") && !blockStart.Token.Equals("elseif"))
        {
            throw new InvalidElseTokenException(SourceCodeLineNumber);
        }

        _blockStart = blockStart;
        BlockStartLineIndex = blockStart.LineIndex;
    }
}