using NUnit.Framework;

namespace PbsTexts.Library.Collections;

public class CollectionsTest: TestBase
{
    private const string Key1 = "\"number\"";
    private const string Key2 = "\"string\"";
    private const string Key3 =  "\"bool\"";
    private const string Key4 =  "\"null\"";
    
    private const string Value1 = "1";
    private const string Value2 = "\"hi!\"";
    private const string Value3 = "false";
    private const string Value4 = "null";
    
    protected override string Code { get; } = @$"
request pbs/debug
request pbs/dict
request pbs/list
debug.traceOn()

var myDict = dict.create()
var myList = list.create()

assert.true(myDict isObject)
assert.save(""0"")
assert.true(myList isObject)
assert.save(""1"")

myDict.set(1,1)

";

    [Test]
    public void TestCorrectExecution()
    {
        
        Assert.True(AssertObject.Results["0"]);
        Assert.True(AssertObject.Results["1"]);
        Assert.True(AssertObject.Results["2"]);
        Assert.True(AssertObject.Results["3"]);
        Assert.True(AssertObject.Results["4"]);
        Assert.True(AssertObject.Results["5"]);
        Assert.True(AssertObject.Results["6"]);
        Assert.True(AssertObject.Results["7"]);
        Assert.True(AssertObject.Results["8"]);
        Assert.True(AssertObject.Results["9"]);
        Assert.True(AssertObject.Results["10"]);
    }
    
}