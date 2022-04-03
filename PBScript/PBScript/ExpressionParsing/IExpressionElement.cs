using PBScript.Interfaces;
namespace PBScript.ExpressionParsing;

public interface IExpressionElement
{
    public bool IsConsistent { get; }
}

public interface IExpressionValue: IExpressionElement
{
    public PbsValue GetValue();
}

public interface IExpressionOperator: IExpressionElement
{
    public bool IsLeftAssociative { get; }
    public int Priority { get; }
    public Stack<PbsValue> Calculate(Stack<PbsValue> valueStack, IPbsEnvironment env);
}

public interface IExpressionParenthesisOperator : IExpressionOperator
{
    public bool IsClosing { get; }
}