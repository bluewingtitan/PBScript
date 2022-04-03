using PBScript.Exception;
using PBScript.ExpressionParsing.Exceptions;
using PBScript.Interfaces;

namespace PBScript.ExpressionParsing.Operators.Mathematical;

public class DivisionOperator: OperatorBase
{
    public override bool IsLeftAssociative => true;
    public override int Priority => 15;
    protected override string Operator { get; } = "/";

    public override ILegalOperatorFilter Filter => LegalOperatorFilter.TwoNumbers;

    protected override Stack<PbsValue> DoCalculation(Stack<PbsValue> valueStack, IPbsEnvironment env)
    {
        var value2 = valueStack.Pop();
        var value1 = valueStack.Pop();

        if (value2.NumberValue is 0)
        {
            throw new DivisionByZeroException();
        }
        
        valueStack.Push(new PbsValue(value1.NumberValue / value2.NumberValue));
        return valueStack;
    }
}