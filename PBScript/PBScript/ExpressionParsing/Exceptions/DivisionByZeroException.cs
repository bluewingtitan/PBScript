using PBScript.Exception;

namespace PBScript.ExpressionParsing.Exceptions;

public class DivisionByZeroException: ExpressionParsingException
{
    public DivisionByZeroException() : base("Tried to divide by zero", "/")
    {
    }
}