using PBScript.Exception;
using PBScript.ExpressionParsing.Exceptions;
using PBScript.Interfaces;

namespace PBScript.ExpressionParsing;

/// <summary>
/// Loose Implementation of the Shunting-Yard Algorithm by Dijkstra.
/// </summary>
public class ExpressionBuilder
{
    private List<IExpressionElement> Output = new List<IExpressionElement>();
    private Stack<IExpressionOperator> Operators = new Stack<IExpressionOperator>();
    
    public void AddElement(IExpressionElement e)
    {
        if (e is IExpressionParenthesisOperator pOp)
        {
            if (!pOp.IsClosing)
            {
                Operators.Push(pOp);
                
                // signal 
                Output.Add(new PbsValue(pOp));
            }
            else
            {
                while (Operators.TryPeek(out var op2) && op2 is not IExpressionParenthesisOperator {IsClosing:false})
                {
                    var poppedOp = Operators.Pop();
                    Output.Add(poppedOp);
                }

                if (Operators.Count == 0)
                {
                    throw new ExpressionParsingException("Mismatched Parenthesis", ")");
                }
                
                // pop the opening parenthesis, forget about them
                var poppedOp2 = Operators.Pop();
            }
        }
        else if (e is IExpressionOperator op)
        {
            while (Operators.TryPeek(out var op2) && (op2.Priority > op.Priority || op.Priority == op2.Priority && op.IsLeftAssociative))
            {
                if (op is IExpressionParenthesisOperator pOpOnStack)
                {
                    if (!pOpOnStack.IsClosing)
                    {
                        break;
                    }
                }
                
                var poppedOp = Operators.Pop();
                
                Output.Add(poppedOp);
            }
            
            Operators.Push(op);
        }
        else
        {
            Output.Add(e);
        }
    }

    public PbsExpression BuildToFinish()
    {
        while (Operators.TryPeek(out var op))
        {
            if (op is IExpressionParenthesisOperator parenthesisOperator)
            {
                throw new ExpressionParsingException("Mismatched Parenthesis", parenthesisOperator.IsClosing?")":"(");
            }
            Output.Add(Operators.Pop());
        }
        
        return new PbsExpression(Output);
    }
    
}