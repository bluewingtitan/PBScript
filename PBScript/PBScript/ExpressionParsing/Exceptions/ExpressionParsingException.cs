namespace PBScript.ExpressionParsing.Exceptions;

public class ExpressionParsingException: System.Exception
{
    public readonly string Reason;
    public readonly string OperatorOrToken;

    public ExpressionParsingException(string reason, string operatorOrToken): base("Parsing or evaluation of expression failed: " + reason + " at token " + operatorOrToken)
    {
        Reason = reason;
        OperatorOrToken = operatorOrToken;
    }
}