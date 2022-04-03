using PBScript.Interfaces;

namespace PBScript.ExpressionParsing.Operators.Logical;

public class NegatingOperator: OperatorBase
{
    public override bool IsLeftAssociative => false;
    public override int Priority => 17;
    protected override string Operator { get; } = "!";
    public override ILegalOperatorFilter Filter => LegalOperatorFilter.ForValueCount(1).Where.OnlyBooleans().Build();
    protected override Stack<PbsValue> DoCalculation(Stack<PbsValue> valueStack, IPbsEnvironment env)
    {
        var value1 = valueStack.Pop();
        
        valueStack.Push(new PbsValue(!value1.BooleanValue));
        return valueStack;
    }
}