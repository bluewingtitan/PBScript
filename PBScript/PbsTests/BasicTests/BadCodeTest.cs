using System;
using NUnit.Framework;
using PBScript.Environment;
using PBScript.Exception;
using PBScript.Interfaces;
using PBScript.Interpretation;
using PBScript.ProgramElements;

namespace PbsTexts.BasicTests;

public class BadCodeTest
{

    [Test]
    public void Test_BadRequests()
    {
        var code = "request";
        Assert.Throws<InvalidRequestException>(() => PbsInterpreter.InterpretProgram(code));

        code = "request 7$_-";
        Assert.Throws<InvalidRequestException>(() => PbsInterpreter.InterpretProgram(code));
    }
    
    
    [Test]
    public void Test_BadConditionals()
    {
        var code = @"if
end";
        Assert.Throws<InvalidConditionException>(() => PbsInterpreter.InterpretProgram(code));

        code = @"while
end";
        Assert.Throws<InvalidConditionException>(() => PbsInterpreter.InterpretProgram(code));
    }
    
    
    [Test]
    public void Test_BadBlock()
    {
        var code = @"end";
        Assert.Throws<UnexpectedBlockEndException>(() => PbsInterpreter.InterpretProgram(code));

        code = @"
if x = 1
else
else
end";
        Assert.Throws<InvalidElseTokenException>(() => PbsInterpreter.InterpretProgram(code));

        code = @"
if x = 1";
        Assert.Throws<UnclosedBlockException>(() => PbsInterpreter.InterpretProgram(code));


        code = @"
while x = 1
else
end
";
        Assert.Throws<InvalidElseTokenException>(() => PbsInterpreter.InterpretProgram(code));
        
        
        code = @"
while x = 1
elseif x = 10
end
";
        Assert.Throws<InvalidElseTokenException>(() => PbsInterpreter.InterpretProgram(code));
        Assert.Throws<UnexpectedBlockEndException>(() => new ElseIfElement().ThrowIfNotValid());
    }
    
    [Test]
    public void Test_BadLine()
    {
        var code = @"++";
        Assert.Throws<InvalidLineException>(() => PbsInterpreter.InterpretProgram(code));
        
        code = @"++var x = 10";
        Assert.Throws<InvalidLineException>(() => PbsInterpreter.InterpretProgram(code));
                                                                                                 Assert.Throws<InvalidLineException>(() => PbsInterpreter.InterpretProgram(code));
    }
    
    [Test]
    public void Test_BadVariable()
    {
        var code = @"var + = 7";
        Assert.Throws<InvalidVariableInitialization>(() => PbsInterpreter.InterpretProgram(code));

        TestCode(code);
        TestCode("var x = 7");
    }

    private void TestCode(string code)
    {
        try
        {
            PbsInterpreter.InterpretProgram(code);
        }
        catch (PbsException e)
        {
            Assert.NotNull(e.Token);
            Assert.NotNull(e.ErrorDescription);
            Assert.NotNull(e.SourceCodeLineNumber);
        }
    }

    [Test]
    public void Test_BadAction()
    {
        var action = new PbsAction("");
        Assert.AreEqual(PbsValue.Null, action.Execute(PbsEnvironment.ProductionReady()));

        PbsInterpreter.Log = true;
        action = new PbsAction("(true && false)");
        Assert.AreEqual(false, action.Execute(PbsEnvironment.ProductionReady()).BooleanValue);
    }
    
    [Test]
    public void Test_BadValue()
    {
        var value = new PbsValue(new EndElement());
        Assert.AreEqual(VariableType.Undefined, value.ReturnType);
    }

}