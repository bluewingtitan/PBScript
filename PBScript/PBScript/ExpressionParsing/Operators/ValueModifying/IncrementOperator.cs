using PBScript.Environment;
using PBScript.Interfaces;

namespace PBScript.ExpressionParsing.Operators.ValueModifying;

public class IncrementOperator: OperatorBase
{
    public override bool IsLeftAssociative => true;
    public override int Priority => 17;
    protected override string Operator => "++";
    public override ILegalOperatorFilter Filter { get; } = LegalOperatorFilter.ForValueCount(1).Where.FirstIsVariableOfType(VariableType.Number).Build();
    
    protected override Stack<PbsValue> DoCalculation(Stack<PbsValue> valueStack, IPbsEnvironment env)
    {
        var value1 = valueStack.Pop();

        value1.SetObjectValue(value1.NumberValue + 1);
        
        valueStack.Push(value1);
        return valueStack;
    }
}