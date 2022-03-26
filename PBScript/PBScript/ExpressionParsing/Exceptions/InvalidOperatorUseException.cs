using PBScript.Exception;

namespace PBScript.ExpressionParsing.Exceptions;

public class InvalidOperatorUseException: ExpressionParsingException
{
    public InvalidOperatorUseException(string op) : base("Wasn't able to parse expression due to invalid operator use.", op)
    { }
}