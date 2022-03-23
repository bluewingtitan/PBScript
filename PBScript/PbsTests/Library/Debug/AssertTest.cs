using NUnit.Framework;

namespace PbsTexts.Library.Debug;

public class AssertTest: TestBase
{
    private const string KeyTrueAssert = "trueassert";
    private const string KeyTrueTrue = "truetrue";
    private const string KeyFalseFalse = "falsefalse";
    private const string KeyNotNullTrue = "notnulltrue";
    private const string KeyNotNullTrue2 = "notnulltrue2";
    private const string KeyNullNull = "nullnull";
    private const string KeyNullNull2 = "nullnull2";
    
    // Negative Tests
    private const string KeyNull1 = "null1";
    private const string KeyNotNullNull = "notnullnull";
    private const string KeyTrueFalse = "truefalse";
    private const string KeyFalseTrue = "falsetrue";
    
    protected override string Code => $@"
request pbs/debug
assert true assert // assert defaults to true
assert save ""{KeyTrueAssert}""

assert true true
assert save ""{KeyTrueTrue}""

assert false false
assert save ""{KeyFalseFalse}""

assert notnull true
assert save ""{KeyNotNullTrue}""

assert notnull 15
assert save ""{KeyNotNullTrue2}""

assert notnull ""some string stuff."" + 15
assert save ""{KeyNotNullTrue2}""

assert null randomObject
assert save ""{KeyNullNull}""

assert null null
assert save ""{KeyNullNull2}""

// negative tests
assert null 1
assert save ""{KeyNull1}""

assert notnull null
assert save ""{KeyNotNullNull}""

assert true false
assert save ""{KeyTrueFalse}""

assert false true
assert save ""{KeyFalseTrue}""
";


    [Test]
    public void Test_AssertsWhereTrue()
    {
        Assert.True(AssertObject.Results[KeyTrueAssert]);
        Assert.True(AssertObject.Results[KeyTrueTrue]);
        Assert.True(AssertObject.Results[KeyFalseFalse]);
        Assert.True(AssertObject.Results[KeyNotNullTrue]);
        Assert.True(AssertObject.Results[KeyNotNullTrue2]);
        Assert.True(AssertObject.Results[KeyNullNull]);
        Assert.True(AssertObject.Results[KeyNullNull2]);
        
        
        Assert.False(AssertObject.Results[KeyNull1]);
        Assert.False(AssertObject.Results[KeyNotNullNull]);
        Assert.False(AssertObject.Results[KeyTrueFalse]);
        Assert.False(AssertObject.Results[KeyFalseTrue]);
    }
    
}