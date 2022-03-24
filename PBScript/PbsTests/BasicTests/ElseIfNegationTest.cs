using NUnit.Framework;

namespace PbsTexts.BasicTests;

/// <summary>
/// ElseIfTest only checks IF the correct blocks get executed.
/// This test also checks that NO OTHER blocks get executed, confirming ElseIfTest by negating other possibilities.
/// </summary>
public class ElseIfNegationTest: TestBase
{
    protected override string Code => @"// row of conditions again, but counter in each block (should still end up at 2)
// only checks that if, else or elseif run exclusively in a block (never more than one of the three) 

var x set 10

if x == 0
    x up
    counter
elseif x == 10
    counter
else
    x up
    counter
end

// x should be 10, would be 9 or even 8 if something went wrong.

if x == 9
    x rmv 2
    counter
elseif x == 8
    x down
    counter
elseif x==10
    counter
else
    x = 7
    counter
end
";

    [Test]
    public void Test_CorrectlyCounted()
    {
        Assert.AreEqual(2, TestCounter.Counter);
    }
    
    
}