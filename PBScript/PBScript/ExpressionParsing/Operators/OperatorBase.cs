using PBScript.Environment;
using PBScript.ExpressionParsing.Exceptions;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ExpressionParsing.Operators;

public abstract class OperatorBase: IExpressionOperator
{
    public bool IsConsistent => false;
    public abstract bool IsLeftAssociative { get; }
    public abstract int Priority { get; }
    protected abstract string Operator { get; }

    public abstract ILegalOperatorFilter Filter { get; }

    public Stack<PbsValue> Calculate(Stack<PbsValue> valueStack, IPbsEnvironment env)
    {
        // => Verify correct usage
        if (valueStack.Count < Filter.ValueCount)
        {
            throw new InvalidOperatorUseException(Operator);
        }
        
        var pbsValues = new PbsValue[Filter.ValueCount];
        for (int i = Filter.ValueCount-1; i >= 0; i--)
        {
            pbsValues[i] = valueStack.Pop();
            
            if (pbsValues[i].ReturnType == VariableType.Token_FunctionClosure)
                i++;
        }

        // Push the values back!
        // (Could also just use the generated array to proceed, but that feels like a bad side-effect
        // that will bite me in the arse at a later point, because I forget about it.
        // TODO: Re-evaluate this decision at a later point.
        foreach (var value in pbsValues)
            valueStack.Push(value);
        
        if (!Filter.Fits(pbsValues))
        {
            throw new InvalidOperatorUseException(Operator);
        }

        if (PbsInterpreter.Log)
            env.Log(Operator, ">> " + String.Concat(pbsValues.Select(x=>x.AsString() + ", ")));
        
        
        return DoCalculation(valueStack, env);
    }

    protected abstract Stack<PbsValue> DoCalculation(Stack<PbsValue> valueStack, IPbsEnvironment env);


    public override string ToString()
    {
        return Operator;
    }
}