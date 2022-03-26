namespace PBScript.ExpressionParsing.Exceptions;

public class ExpressionEvaluationException: ExpressionParsingException
{
    public ExpressionEvaluationException(string reason, string operatorOrToken) : base(reason, operatorOrToken)
    {
    }
}