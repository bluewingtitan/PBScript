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

var d = dict.create()
var l = list.create()

assert.true(dict.create() isObject)
assert.save(""0"")
assert.true(list.create() isObject)
assert.save(""1"")

// first, establish random other values to check correct replacement
d.set({Key3} null)
d.set({Key2} null)
d.set({Key1} null)
d.set({Key4} {Value4})

d.set({Key1} {Value1})
d.set({Key2} {Value2})
d.set({Key3} {Value3})
d.set({Key4} {Value4})


assert.true(d.get({Key1}) == {Value1})
assert.save(""2"")
assert.true(d.get({Key2}) == {Value2})
assert.save(""3"")
assert.true(d.get({Key3}) == {Value3})
assert.save(""4"")
assert.true(d.get({Key4}) == {Value4})
assert.save(""5"")

assert.true(d.count() == 4)
assert.save(""6"")
d.delete({Key1})
assert.true(d.count() == 3)
assert.save(""7"")



// list tests
l.add({Value1})
l.add({Value1})
l.add({Value1})
l.add({Value1})

l.set(1 {Value2})
l.set(2 {Value3})
l.set(3 {Value4})

assert.true(l.get(0) == {Value1})
assert.save(""8"")
assert.true(l.get(1) == {Value2})
assert.save(""9"")
assert.true(l.get(2) == {Value3})
assert.save(""10"")
assert.true(l.get(3) == {Value4})
assert.save(""11"")

assert.true(l.count() == 4)
assert.save(""12"")

l.delete(1)
l.delete(1)
assert.true(l.count() == 2)
assert.save(""13"")
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
        Assert.True(AssertObject.Results["11"]);
        Assert.True(AssertObject.Results["12"]);
        Assert.True(AssertObject.Results["13"]);
    }
    
}