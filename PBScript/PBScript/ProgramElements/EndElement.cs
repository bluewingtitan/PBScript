using PBScript.Exception;
using PBScript.Interfaces;

namespace PBScript.ProgramElements;

public class EndElement: ElementBase, IPbsBlockEnd
{
    public override string Token { get; protected set; } = "end";
    
    public override int Execute(IPbsEnvironment env)
    {
        if (_blockStart == null)
            return LineIndex + 1;
        
        if (!_blockStart.LastConditionMet())
            return LineIndex + 1;

        switch (_blockStart.Token)
        {
            case "while":
                return _blockStart.LineIndex;
            
            default:
                return LineIndex + 1;
        }
    }

    public override bool CheckValid()
    {
        if (_blockStart == null)
        {
            throw new UnexpectedBlockEndException(Token, SourceCodeLineNumber);
        }

        return true;
    }

    public int BlockStartLineIndex { get; private set; }
    private IPbsBlockStart? _blockStart;
    public void RegisterBlockStart(IPbsBlockStart blockStart)
    {
        _blockStart = blockStart;
        BlockStartLineIndex = blockStart.LineIndex;
    }
}