using System.Text;
using PBScript.Environment;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ExpressionParsing.Operators.ValueModifying;

public class AssignOperator: OperatorBase
{
    public override bool IsLeftAssociative => false;
    public override int Priority => 4;
    protected override string Operator => "=";
    public override ILegalOperatorFilter Filter { get; } = LegalOperatorFilter.ForValueCount(2).Where.FirstIsVariable().Build();
    
    protected override Stack<PbsValue> DoCalculation(Stack<PbsValue> valueStack, IPbsEnvironment env)
    {
        var value2 = valueStack.Pop();
        var value1 = valueStack.Pop();

        
        if (PbsInterpreter.Log)
            env.Log(Operator, value1 + " = " + value2);
        
        value1.SetObjectValue(value2.ObjectValue);
        
        valueStack.Push(value1);
        return valueStack;
    }
}