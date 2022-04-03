using System.Text;
using PBScript.ExpressionParsing.Exceptions;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ExpressionParsing;

/// <summary>
/// Represents a fully parsed expression, serialized in a queue as Reverse Polish Notation.
/// </summary>
public class PbsExpression
{
    // Just a shorthand, as it looks cleaner in some contexts.
    public static PbsExpression Parse(string expressionText) => ExpressionParser.Parse(expressionText);
    

    private List<IExpressionElement> _expressionElements;

    public PbsExpression(List<IExpressionElement> expressionElements)
    {
        _expressionElements = expressionElements;
    }

    public PbsValue Evaluate(IPbsEnvironment env)
    {
        try
        {
            if (PbsInterpreter.Log)
            {
                env.Log("EXPRESSION", "Evaluate: -----[" + ToString() + "]--------------");
            }
            
            var valueStack = new Stack<PbsValue>();

            foreach (var element in _expressionElements)
            {
                if (element is IExpressionOperator o)
                {
                    if (PbsInterpreter.Log)
                    {
                        env.Log("EXPRESSION", "Exec " + o);
                    }
                    valueStack = o.Calculate(valueStack, env);
                } else if (element is IExpressionValue v)
                {
                    if (PbsInterpreter.Log)
                    {
                        env.Log("EXPRESSION", "Push " + v);
                    }
                
                    valueStack.Push(v.GetValue());
                }
            }

            return valueStack.TryPop(out var result)? result : PbsValue.Null;
        }
        catch (System.Exception e)
        {
            throw new ExpressionEvaluationException(e.Message, "");
        }
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        foreach (var element in _expressionElements)
        {
            builder.Append(element).Append(' ');
        }

        return builder.ToString();
    }
}