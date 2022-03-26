using PBScript.Interfaces;

namespace PBScript.ExpressionParsing.Operators.Comparing;

public class BiggerThanOperator: OperatorBase
{
    public override bool IsLeftAssociative => true;
    public override int Priority => 11;
    protected override string Operator { get; } = ">";
    public override ILegalOperatorFilter Filter => LegalOperatorFilter.TwoNumbers;
    protected override Stack<PbsValue> DoCalculation(Stack<PbsValue> valueStack, IPbsEnvironment env)
    {
        var value2 = valueStack.Pop();
        var value1 = valueStack.Pop();

        valueStack.Push(new PbsValue(value1.NumberValue > value2.NumberValue));
        return valueStack;
    }
}