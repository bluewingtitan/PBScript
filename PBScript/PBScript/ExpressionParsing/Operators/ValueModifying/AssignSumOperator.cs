using System.Text;
using PBScript.Environment;
using PBScript.Interfaces;

namespace PBScript.ExpressionParsing.Operators.ValueModifying;

public class AssignSumOperator: OperatorBase
{
    public override bool IsLeftAssociative => false;
    public override int Priority => 4;
    protected override string Operator => "+=";
    public override ILegalOperatorFilter Filter { get; } = LegalOperatorFilter.ForValueCount(2)
        .Where.FirstIsVariableOfType(VariableType.Number).And.OnlyNumbers()
        .Or.FirstIsVariableOfType(VariableType.String)
        .Build();
    
    protected override Stack<PbsValue> DoCalculation(Stack<PbsValue> valueStack, IPbsEnvironment env)
    {
        var value2 = valueStack.Pop();
        var value1 = valueStack.Pop();

        object? result = null;
        if (value1.ReturnType == VariableType.String || value2.ReturnType == VariableType.String)
        {
            var builder = new StringBuilder();
            builder.Append(value1.ObjectValue);
            builder.Append(value2.ObjectValue);

            result = builder.ToString();
        }
        else
        {
            result = value1.NumberValue + value2.NumberValue;
        }
        
        value1.SetObjectValue(result);
        
        valueStack.Push(value1);
        return valueStack;
    }
}