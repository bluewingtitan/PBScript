using NUnit.Framework;

namespace PbsTexts.LanguageFeatures;

public class LabelTest: TestBase
{
    protected override string Code => @"
goto firstLabel

counter

label firstLabel

goto end

counter";

    [Test]
    public void Test_NoCounterTriggered()
    {
        Assert.AreEqual(0, _testCounter.Counter);
    }
}