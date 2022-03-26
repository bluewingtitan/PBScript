using System.Collections.Immutable;
using System.Text;
using System.Text.RegularExpressions;
using PBScript.Environment;
using PBScript.ExpressionParsing.Exceptions;
using PBScript.ExpressionParsing.Operators;
using PBScript.ExpressionParsing.Operators.Comparing;
using PBScript.ExpressionParsing.Operators.Logical;
using PBScript.ExpressionParsing.Operators.Mathematical;
using PBScript.ExpressionParsing.Operators.ValueModifying;
using PBScript.Interfaces;

namespace PBScript.ExpressionParsing;

public struct ExpressionContainer
{
    public string Expression { get; init; }
}

public static class ExpressionParser
{
    public static bool TraceParsing = false;
    
    #region Readonly Helpers

    private static readonly ImmutableDictionary<string, IExpressionOperator> Operators =
        GetOperators().ToImmutableDictionary();

    private static readonly Regex OperatorRegex = BuildOperatorRegex();


    private static Dictionary<string, IExpressionOperator> GetOperators()
    {
        Dictionary<string, IExpressionOperator> dict = new();

        
        dict.Add("(", ParenthesisOperator.OpeningParenthesis);
        dict.Add(")", ParenthesisOperator.ClosingParenthesis);

        #region Is Operators

        dict.Add("isNumber", new IsOperator(VariableType.Number));
        dict.Add("isNull", new IsOperator(VariableType.Null));
        dict.Add("isString", new IsOperator(VariableType.String));
        dict.Add("isBoolean", new IsOperator(VariableType.Boolean));
        dict.Add("isUndefined", new IsOperator(VariableType.Undefined));

        #endregion
        
        #region Comparing
        
        dict.Add(">", new BiggerThanOperator());
        dict.Add("<", new SmallerThanOperator());
        dict.Add("==", new EqualityOperator());
        dict.Add("!=", new InequalityOperator());
        dict.Add(">=", new BiggerThenOrEqualToOperator());
        dict.Add("<=", new SmallerThanOrEqualToOperator());
        
        #endregion

        #region Logical

        dict.Add("&&", new AndOperator());
        dict.Add("||", new OrOperator());
        dict.Add("!", new NegatingOperator());
        
        #endregion

        #region Mathematical
        
        dict.Add("+", new AdditionOperator());
        dict.Add("-", new SubtractionOperator());
        dict.Add("*", new MultiplicationOperator());
        dict.Add("/", new DivisionOperator());
        dict.Add("%", new ModuloOperator());

        #endregion

        #region ValueModifying

        dict.Add("=", new AssignOperator());
        dict.Add("+=", new AssignSumOperator());
        dict.Add("-=", new AssignDifferenceOperator());
        dict.Add("*=", new AssignProductOperator());
        dict.Add("/=", new AssignQuotientOperator());
        dict.Add("%=", new AssignModuloOperator());
        dict.Add("++", new IncrementOperator());
        dict.Add("--", new DecrementOperator());

        #endregion
        
        return dict;
    }

    private static Regex BuildOperatorRegex()
    {
        var regexString = "(";

        foreach (var (op, _) in Operators.OrderByDescending(kvp => kvp.Key.Length).ToDictionary(x=> x.Key, x=> x.Value))
        {
            regexString += Regex.Escape(op) + "|";
        }
        // remove last |
        regexString = regexString.Remove(regexString.Length - 1) + ")";

        return new Regex(regexString, RegexOptions.Compiled);
    }


    #endregion

    #region Parsing Functions

    private delegate IExpressionElement ParseElementDelegate(ExpressionContainer expression, int nextOperatorIndex,
        int currentIndex, out int newIndex);

    private delegate bool FitsElementTypeDelegate(char startChar);

    private static readonly Dictionary<FitsElementTypeDelegate, ParseElementDelegate> ReaderFunctions = new()
    {
        {c => c == '"', ReadStringLiteral},
        {c => NumericValues.Contains(c), ReadDouble},
    };

    private static readonly ParseElementDelegate StandardParser = ReadAction;
    

    #endregion


    public static PbsExpression Parse(string expressionString)
    {
        if (TraceParsing)
        {
            Console.WriteLine("-----------[PARSE:<" + expressionString + ">]-----------");
        }
        
        var builder = new ExpressionBuilder();
        var expr = new ExpressionContainer {Expression = expressionString};
        var operators = new Queue<Match>(OperatorRegex.Matches(expressionString));
        operators.TryDequeue(out var nextOp);

        var index = 0;

        while (index < expr.Expression.Length)
        {
            if (char.IsWhiteSpace(expr.Expression[index]))
            {
                // Skip whitespace between expressions.
                index++;
                continue;
            }
            
            
            if (nextOp != null && nextOp.Index<=index)
            {
                if (nextOp.Index < index)
                {
                    // the last expression-element probably was a string literal (=> Operators inside that will be skipped of course!).
                    operators.TryDequeue(out nextOp);
                    continue;
                }
                
                // get operator
                var @operator = Operators[nextOp.Value];
                if(TraceParsing) Console.WriteLine("Parse ["+ nextOp.Value + "] " + index + " >> " + (index + nextOp.Value.Length));
                builder.AddElement(@operator);
                index = nextOp.Index + nextOp.Value.Length;
                
                operators.TryDequeue(out nextOp);
                
                continue;
            }

            ParseElementDelegate? parser = null;
            int newIndex;
            foreach (var (filter, candidate) in ReaderFunctions)
            {
                if (!filter(expr.Expression[index])) continue;
                
                parser = candidate;
                break;
            }

            parser ??= StandardParser;

            var element = parser(expr, nextOp?.Index ?? expr.Expression.Length, index, out newIndex);

            if(TraceParsing) Console.WriteLine("Parse [" + element.GetType().Name + "] " + index + " >> " + newIndex);
            
            builder.AddElement(element);
            index = newIndex;
        }

        var result = builder.BuildToFinish();
        if (TraceParsing)
        {
            Console.WriteLine(result.ToString());
        }

        return result;
    }


    private static IExpressionElement ReadAction(ExpressionContainer e, int nextOpIndex,
        int currentIndex, out int newIndex)
    {
        var sb = new StringBuilder();

        var tokenRead = false;
        var startReadAction = false;
        
        while (currentIndex < nextOpIndex)
        {
            if (Char.IsWhiteSpace(e.Expression[currentIndex]) || e.Expression[currentIndex] == '.')
            {
                if (tokenRead && startReadAction)
                {
                    break;
                }

                tokenRead = true;
            }
            else if(tokenRead)
                startReadAction = true;
            
            
            sb.Append(e.Expression[currentIndex]);
            currentIndex++;
        }

        newIndex = currentIndex;
        
        return new ExpressionActionElement(sb.ToString());
    }
    
    private static IExpressionElement ReadStringLiteral(ExpressionContainer e, int nextOpIndex,
        int currentIndex, out int newIndex)
    {
        var sb = new StringBuilder();
        currentIndex++; // move behind the first " (Start of the literal)
        while (currentIndex < e.Expression.Length && e.Expression[currentIndex] != '"')
        {
            sb.Append(e.Expression[currentIndex]);
            currentIndex++;
        }
        newIndex = currentIndex+1; // +1 moves behind the ".
        return new PbsValue(sb.ToString()); 
    }

    private static IExpressionElement ReadDouble(ExpressionContainer e, int nextOpIndex,
        int currentIndex, out int newIndex)
    {
        var sb = new StringBuilder();

        if (e.Expression[currentIndex] == '~')
        {
            sb.Append('-');
            currentIndex++;
        }

        // Support for numbers like .55 (instead of 0.55)
        if (e.Expression[currentIndex] == '.')
        {
            sb.Append(0);
        }
        
        while (currentIndex < nextOpIndex && NumericValues.Contains(e.Expression[currentIndex]))
        {
            sb.Append(e.Expression[currentIndex]);
            currentIndex++;
        }
        newIndex = currentIndex;

        try
        {
            return new PbsValue(double.Parse(sb.ToString()));
        }
        catch (System.Exception)
        {
            throw new ExpressionParsingException("Bad number format. Does the number contain multiple dots?", sb.ToString());
        }
        
    }

    private static readonly HashSet<char> NumericValues = new()
    {
        '.','~',
        '0','1','2','3','4','5','6','7','8','9',
    };
}