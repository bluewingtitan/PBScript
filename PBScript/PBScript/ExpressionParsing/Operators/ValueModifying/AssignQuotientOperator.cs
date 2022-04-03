using PBScript.Environment;
using PBScript.ExpressionParsing.Exceptions;
using PBScript.Interfaces;

namespace PBScript.ExpressionParsing.Operators.ValueModifying;

public class AssignQuotientOperator: OperatorBase
{
    public override bool IsLeftAssociative => false;
    public override int Priority => 4;
    protected override string Operator => "/=";
    public override ILegalOperatorFilter Filter { get; } = LegalOperatorFilter.ForValueCount(2)
        .Where.FirstIsVariableOfType(VariableType.Number).And.OnlyNumbers().Build();
    
    protected override Stack<PbsValue> DoCalculation(Stack<PbsValue> valueStack, IPbsEnvironment env)
    {
        var value2 = valueStack.Pop();
        var value1 = valueStack.Pop();

        if (value2.NumberValue is 0)
        {
            throw new DivisionByZeroException();
        }

        value1.SetObjectValue(value1.NumberValue / value2.NumberValue);
        
        valueStack.Push(value1);
        return valueStack;
    }
}