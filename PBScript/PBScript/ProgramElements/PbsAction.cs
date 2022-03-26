using PBScript.Exception;
using PBScript.ExpressionParsing;
using PBScript.ExpressionParsing.Exceptions;
using PBScript.Interfaces;

namespace PBScript.ProgramElements;

public class PbsAction : IPbsAction
{
    private readonly PbsExpression _pbsExpression;

    public PbsAction(string actionText)
    {
        _pbsExpression = ExpressionParser.Parse(actionText);
    }
    
    public PbsValue Execute(IPbsEnvironment env)
    {
        return _pbsExpression.Evaluate(env);
    }
}