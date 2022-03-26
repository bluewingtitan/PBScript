using PBScript.Interfaces;

namespace PBScript.ExpressionParsing.Operators;

public class ParenthesisOperator: IExpressionParenthesisOperator, IExpressionValue
{
    public static readonly ParenthesisOperator OpeningParenthesis = new (false);
    public static readonly ParenthesisOperator ClosingParenthesis = new (true); 
    
    protected ParenthesisOperator(bool isClosing)
    {
        IsClosing = isClosing;
        
    }
    
    public bool IsConsistent => true;
    public PbsValue GetValue()
    {
        return PbsValue.Null;
    }

    public bool IsLeftAssociative => false;
    public int Priority => 1;

    public override string ToString()
    {
        return IsClosing ? ")" : "(";
    }

    public Stack<PbsValue> Calculate(Stack<PbsValue> valueStack, IPbsEnvironment _)
    {
        // Should not be called, but if it is called, we simply skip.
        return valueStack;
    }
    public bool IsClosing { get; }
}