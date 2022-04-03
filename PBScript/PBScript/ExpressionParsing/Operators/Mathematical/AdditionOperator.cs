using System.Text;
using PBScript.Environment;
using PBScript.Interfaces;

namespace PBScript.ExpressionParsing.Operators.Mathematical;

public class AdditionOperator: OperatorBase
{
    public override bool IsLeftAssociative => true;
    public override int Priority => 14;
    protected override string Operator => "+";
    public override ILegalOperatorFilter Filter { get; } = LegalOperatorFilter.ForValueCount(2)
        .Where.MinimumStringAmount(1)
        .Or.OnlyNumbers()
        .Build();

    protected override Stack<PbsValue> DoCalculation(Stack<PbsValue> valueStack, IPbsEnvironment env)
    {
        var value2 = valueStack.Pop();
        var value1 = valueStack.Pop();

        if (value1.ReturnType == VariableType.String || value2.ReturnType == VariableType.String)
        {
            var result = new StringBuilder();
            result.Append(value1.ObjectValue);
            result.Append(value2.ObjectValue);
            valueStack.Push(new PbsValue(result.ToString()));
            return valueStack;
        }
        
        // => we got two numeric values!
        
        valueStack.Push(new PbsValue(value1.NumberValue + value2.NumberValue));
        return valueStack;
    }
}