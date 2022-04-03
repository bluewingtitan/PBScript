using System;
using System.Collections.Generic;
using NUnit.Framework;
using PBScript.Environment;
using PBScript.ExpressionParsing;
using PBScript.ExpressionParsing.Operators.Comparing;
using PBScript.ExpressionParsing.Operators.Mathematical;
using PBScript.ExpressionParsing.Operators.ValueModifying;
using PBScript.Interfaces;

namespace PbsTexts.Expressions;

public class BasicOperatorTests
{
    [Test]
    public void Test_Assignment()
    {
        var env = PbsEnvironment.ProductionReady();
        var variable = new VariableObject("x");

        var op = new AssignOperator();

        var stack = new Stack<PbsValue>();
        stack.Push(variable.ExecuteAction("", Array.Empty<PbsValue>(), env));
        stack.Push(new PbsValue(10));

        op.Calculate(stack, env);
        
        Assert.AreEqual(variable.Value, stack.Pop());
    }
    
    
    
    [Test]
    public void Test_MegaEquations()
    {
        var env = PbsEnvironment.ProductionReady();
        
        Assert.DoesNotThrow(() => ExpressionParser.Parse("(2 + 10/2*2 - 10) == 2 && " +
                                                                "(true isNumber || false isBoolean || 7 isNumber && !(1 isNull)) && " +
                                                                "((1 += 2) < 4 && 6 > 4) &&" +
                                                                "5%2==1").Evaluate(env));
        
        Assert.DoesNotThrow(() => ExpressionParser.Parse(
            "(3 %= 2) == 1 && 1 < 2 && 2 <= 2 && 2 >= 2 && 3 > 2 && 3 != \"Hi!\" || 5 == 5"
            ).Evaluate(env));
    }
    
}