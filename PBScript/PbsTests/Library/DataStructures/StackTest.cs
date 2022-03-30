using NUnit.Framework;
using PBScript.Environment;
using PBScript.Environment.DataStructures;
using PBScript.Interfaces;

namespace PbsTexts.Library.DataStructures;

public class StackTest: TestBase
{
    private const string Value1 = "1";
    private const string Value2 = "\"hi\"";
    private const string Value3 = "false";
    private const string Value4 = "null";
    
    protected override string Code => $@"
request pbs/debug
request pbs/stack
debug traceOn

var stk = stack.create()

assert true(stack create() isObject)
assert save(""prepare0"")

assert null(stack)
assert save(""prepare2"")

stk push({Value1})
stk push({Value2})
stk push({Value3})
stk push({Value4})


assert null(stk pop())
assert save (""0"")

// top value is false now, so this should be true
assert false (stk pop)
assert save (""1"")

assert true (stk pop == {Value2})
assert save (""2"")

assert true (stk pop == {Value1})
assert save (""3"")

assert true (stk count == 0)
assert save (""4"")

stk push ({Value1})
stk push ({Value1})
stk push ({Value1})
assert true (stk count == 3)
assert save ""5""

stk clear
assert true (stk count == 0)
assert save ""6""


stk push ({Value3}) // == false
assert false (stk peek)
assert save (""7"")
assert false stk peek
assert save (""8"")
assert.true(stk == stk.peek())
assert save ""9""
assert false (stk push())
assert save ""10""
";


    [Test]
    public void Test_ElementsWhereStackedCorrectly()
    {
        Assert.True(AssertObject.Results["prepare0"]);
        Assert.True(AssertObject.Results["prepare2"]);
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

    [Test]
    public void Test_Documentation()
    {
        IPbsObject creator = new CreatorObject("stack", s => new StackObject(s));
        Assert.NotNull(creator.GetDocumentation());
        Assert.NotNull(creator.ExecuteAction("create", new PbsValue[] { new PbsValue("xStack") }, new PbsEnvironment()));

        IPbsObject stack = new StackObject("stk");
        Assert.NotNull(stack.GetDocumentation());
    }
    
}