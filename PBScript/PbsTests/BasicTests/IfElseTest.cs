using NUnit.Framework;

namespace PbsTexts.BasicTests;

public class IfElseTest: TestBase
{
    protected override string Code => $@"// tests counting against the test-counter.
// counter will count every time a if expression is true
var x = 0

if x == counter // should be false (counter call will ++ counter, so this is 0 == 1)
    counter
end
// x should still be 0 now.
x++

if x > counter // should be false => counter == 2
    x--
    counter
end
// x should (again) be 0 now.

// reset x
x = 0

if x==0 // should be true => counter == 3
    counter
else
    x++
end
// x should still be 0 now

if x==0 // should be true => counter == 4
    counter
end

if x==10 // should be false => counter == 4
    counter
else
    x++
end
";

    [Test]
    public void Test_CountedCorrectly()
    {
        Assert.AreEqual(4, TestCounter.Counter);
    }
    
}