using PBScript.Interfaces;

namespace PBScript.Environment.DataStructures;

public class QueueObject: ObjectBase
{
    private readonly Queue<PbsValue> _queue = new();

    public QueueObject(string objectName)
    {
        ObjectName = objectName;
        
        Register("dequeue", Dequeue);
        Register("peek", Peek);
        Register("clear", Clear);
        Register("count", Count);
        Register("enqueue", Enqueue);
    }

    protected override PbsValue DefaultAction(string command, PbsValue[] param, IPbsEnvironment env)
    {
        return Peek(null, null);
    }

    #region Operations

    private PbsValue Count(PbsValue[] param, IPbsEnvironment env)
    {
        return new PbsValue(_queue.Count);
    }
    
    private PbsValue Dequeue(PbsValue[] param, IPbsEnvironment env)
    {
        return _queue.Count>0 ? _queue.Dequeue() : PbsValue.Null;
    }

    private PbsValue Peek(PbsValue[]? param, IPbsEnvironment? env)
    {
        return _queue.Count>0 ? _queue.Peek() : PbsValue.Null;
    }
    
    private PbsValue Enqueue(PbsValue[] value, IPbsEnvironment env)
    {
        if (value.Length < 1)
        {
            return PbsValue.False;
        }
        
        _queue.Enqueue(value[0]);
        return PbsValue.True;
    }
    
    private PbsValue Clear(PbsValue[] param, IPbsEnvironment env)
    {
        _queue.Clear();
        return PbsValue.True;
    }
    
    #endregion

    public override string GetDocumentation()
    {
        return @"Just a basic queue.
Use 'queue create newQueue' to create a new queue with the name 'name'.

Use 'var x = newQueue dequeue' to dequeue the front element and set it to a variables value
Use 'newQueue enqueue x' to enqueue the variable x.
Use 'newQueue clear' to clear the queue.
Use 'newQueue' or 'newQueue peek' to get the front element without removing it.
Use 'newQueue count' to get the amount of items in the queue.";
    }

    public override string ObjectName { get; }
}