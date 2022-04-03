using PBScript.Interfaces;

namespace PBScript.ExpressionParsing.Operators.Comparing;

public class InequalityOperator: OperatorBase
{
    public override bool IsLeftAssociative => true;
    public override int Priority => 10;
    protected override string Operator { get; } = "!=";

    public override ILegalOperatorFilter Filter => LegalOperatorFilter.ForValueCount(2).Build();

    protected override Stack<PbsValue> DoCalculation(Stack<PbsValue> valueStack, IPbsEnvironment env)
    {
        var value2 = valueStack.Pop();
        var value1 = valueStack.Pop();
        var result = value1.ReturnType == value2.ReturnType;

        if (result)
        {
            if (value1.NumberValue is { } d1 && value2.NumberValue is { } d2)
            {
                // Avoid Floating-Point bs destroying stuff too often (while still maintaining some good accuracy) 
                // This is a beginners language for games after all.
                result = Math.Abs(d1 - d2) < 0.0000001d;
            }
            else
            {
                result = value1.ObjectValue == value2.ObjectValue;
            }
        }
        
        valueStack.Push(new PbsValue(!result));
        return valueStack;
    }
}