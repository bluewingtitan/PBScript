using System;
using NUnit.Framework;
using PBScript.Environment;

namespace PbsTexts.BasicTests;

public class WhileTest: TestBase
{
    protected override string Code => $@"// creates a simple while loop that counts down from 10 to 0.
// uses counter to verify the correct amount of loops done.

var x = 10

while x > 0
    x--
    counter
end";

    [Test]
    public void Test_CorrectAmountOfLoops()
    {
        Assert.AreEqual(10, TestCounter.Counter);
    }

    [Test]
    public void Test_VariableCountedToZero()
    {
        var x1 = Environment.GetObject("x") as VariableObject;
        Assert.NotNull(x1);
        Assert.AreEqual(VariableType.Number, x1.ValueType);
        Assert.NotNull(x1.Value.NumberValue);
        Assert.True(Math.Abs((double)x1.Value.NumberValue - 0) < 0.01);
    }
    
}