using NUnit.Framework;

namespace PbsTexts.BasicTests;

public class ElseIfTest: TestBase
{
    protected override string Code => @"// row of conditions
var y = 120+10

var x = 10

if (x == 0)
    x ++
elseif (x==10)
    counter
else
    x --
end

if x == 9
    x -= 2
elseif (x == 8)
    x down
elseif (x == 10)
    counter
else
    x = 7
end
";

    [Test]
    public void Test_CorrectlyCounted()
    {
        Assert.AreEqual(2, TestCounter.Counter);
    }
}