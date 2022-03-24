using PBScript.Exception;
using PBScript.Interfaces;

namespace PBScript.ProgramElements;

public class EndElement: ElementBase, IPbsBlockEnd
{
    public override string Token { get; } = "end";
    
    public override int Execute(IPbsEnvironment env)
    {
        if (_blockStart == null)
            throw new UnexpectedBlockEndException(Token, SourceCodeLineNumber);
        
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
        _blockStart = blockStart;
    }
}