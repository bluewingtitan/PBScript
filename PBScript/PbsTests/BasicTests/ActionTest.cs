using NUnit.Framework;

namespace PbsTexts.BasicTests;

public class ActionTest: TestBase
{
    protected override string Code => @"counter add
counter
if counter one two three
end
counter 238312213132test123";

    [Test]
    public void Test_CounterWorked()
    {
        Assert.AreEqual(4, TestCounter.Counter);
    }
}