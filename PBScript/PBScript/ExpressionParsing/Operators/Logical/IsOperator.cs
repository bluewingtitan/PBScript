using PBScript.Environment;
using PBScript.Interfaces;

namespace PBScript.ExpressionParsing.Operators.Logical;

public class IsOperator: OperatorBase
{
    private readonly VariableType _type;

    public IsOperator(VariableType type)
    {
        _type = type;
        Operator = "is" + type;
    }
    
    public override bool IsLeftAssociative => true;
    public override int Priority => 10;
    protected override string Operator { get; }
    public override ILegalOperatorFilter Filter => LegalOperatorFilter.ForValueCount(1).Build();
    protected override Stack<PbsValue> DoCalculation(Stack<PbsValue> valueStack, IPbsEnvironment env)
    {
        var value1 = valueStack.Pop();
        
        valueStack.Push(new PbsValue(value1.ReturnType == _type));
        return valueStack;
    }
}