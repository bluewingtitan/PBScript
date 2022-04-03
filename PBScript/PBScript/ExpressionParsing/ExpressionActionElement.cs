using System.Text.RegularExpressions;
using PBScript.Environment;
using PBScript.Exception;
using PBScript.ExpressionParsing.Exceptions;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ExpressionParsing;

public class ExpressionActionElement: IExpressionOperator
{
    private readonly string _actionCode;

    public ExpressionActionElement(string actionCode)
    {
        // Action Structure:
        // 1. is Token
        // 2. is Action-Name (If there)
        // Following String is just pumped into the object itself
        
        var matches = Regex.Matches(actionCode, PbsInterpreter.TokenRegex);

        if (matches.Count > 0 && matches[0].Index == 0)
        {
            _objectToken = matches[0].Value.Trim();
        }
        else
        {
            throw new ExpressionParsingException("Was not able to get action token", actionCode);
        }

        if (matches.Count > 1)
        {
            _actionName = matches[1].Value.Trim();
        }
        else
        {
            _actionName = "";
        }

        _actionCode = _objectToken + " " + _actionName;
    }

    public override string ToString()
    {
        return _actionCode;
    }

    private readonly string _objectToken;
    private readonly string _actionName;

    public bool IsConsistent => false;

    public bool IsLeftAssociative => false;
    public int Priority { get; } = 20;
    public Stack<PbsValue> Calculate(Stack<PbsValue> valueStack, IPbsEnvironment env)
    {
        var obj = env.GetObject(_objectToken);
        
        if (obj != null)
        {
            List<PbsValue> values = new List<PbsValue>();
            if (!string.IsNullOrWhiteSpace(_actionName))
            {
                while (valueStack.Count > 0 && valueStack.Peek().ReturnType != VariableType.Token_FunctionClosure)
                {
                    values.Insert(0, valueStack.Pop());
                }
            }
            
            var result = obj.ExecuteAction(_actionName, values.ToArray(), env);
            valueStack.Push(result);
            
            if(PbsInterpreter.Log) env.Log("Action<"+_actionCode+">", "=> " + result.AsString());
        }
        
        return valueStack;
    }
}