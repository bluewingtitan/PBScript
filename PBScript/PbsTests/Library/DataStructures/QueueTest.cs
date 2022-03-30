using NUnit.Framework;
using PBScript.Environment;
using PBScript.Environment.DataStructures;
using PBScript.Interfaces;

namespace PbsTexts.Library.DataStructures;

public class QueueTest: TestBase
{
    private const string Value1 = "1";
    private const string Value2 = "\"hi!\"";
    private const string Value3 = "false";
    private const string Value4 = "null";
    
    protected override string Code => $@"
request pbs/debug
request pbs/queue
debug.traceOn()

var q = queue.create()

assert true (queue create() isObject)
assert save ""prepare0""

assert null queue
assert save ""prepare2""

q enqueue {Value1}
q enqueue {Value2}
q enqueue {Value3}
q enqueue {Value4}

assert true (q dequeue() == {Value1})
assert save ""0""

assert true (q dequeue() == {Value2})
assert save ""1""

// front value is false now, so this should be true
assert false q dequeue
assert save ""2""

assert null q dequeue
assert save ""3""


assert true (q count() == 0)
assert save ""4""

q enqueue {Value1}
q enqueue {Value1}
q enqueue {Value1}
debug traceOn
assert true (q count == 3)
assert save ""5""

q clear
assert true (q count == 0)
assert save ""6""


q enqueue {Value3} // == false
assert false q peek
assert save ""7""
assert false q peek
assert save ""8""
assert true (q == q peek())
assert save ""9""
assert false (q enqueue())
assert save ""10""
";


    [Test]
    public void Test_ElementsWhereQueuedCorrectly()
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
        IPbsObject queue = new QueueObject("q");
        Assert.NotNull(queue.GetDocumentation());
    }
    
}