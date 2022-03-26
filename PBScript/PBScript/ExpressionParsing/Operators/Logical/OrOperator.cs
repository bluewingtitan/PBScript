using PBScript.Interfaces;

namespace PBScript.ExpressionParsing.Operators.Logical;

public class OrOperator: OperatorBase
{
    public override bool IsLeftAssociative => true;
    public override int Priority => 5;
    protected override string Operator { get; } = "||";
    public override ILegalOperatorFilter Filter => LegalOperatorFilter.TwoBooleans;
    protected override Stack<PbsValue> DoCalculation(Stack<PbsValue> valueStack, IPbsEnvironment env)
    {
        var value2 = valueStack.Pop();
        var value1 = valueStack.Pop();
        
        valueStack.Push(new PbsValue((value1.BooleanValue ?? false) || (value2.BooleanValue ?? false)));
        return valueStack;
    }
}