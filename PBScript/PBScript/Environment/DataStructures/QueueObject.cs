using PBScript.Interfaces;

namespace PBScript.Environment.DataStructures;

public class QueueObject: ObjectBase
{
    private readonly Queue<IPbsValue> _queue = new();

    public QueueObject(string objectName)
    {
        ObjectName = objectName;
        
        Register("dequeue", Dequeue);
        Register("peek", Peek);
        Register("clear", Clear);
        Register("count", Count);
        RegisterTyped("enqueue", Enqueue);
    }

    protected override IPbsValue DefaultAction(string param)
    {
        return Peek("", null);
    }

    #region Operations

    private IPbsValue Count(string param, IPbsEnvironment env)
    {
        return new PbsValue(_queue.Count);
    }
    
    private IPbsValue Dequeue(string param, IPbsEnvironment env)
    {
        return _queue.Count>0 ? _queue.Dequeue() : PbsValue.Null;
    }

    private IPbsValue Peek(string param, IPbsEnvironment? env)
    {
        return _queue.Count>0 ? _queue.Peek() : PbsValue.Null;
    }
    
    private IPbsValue Enqueue(IPbsValue value, IPbsEnvironment env)
    {
        _queue.Enqueue(value);
        return PbsValue.True;
    }
    
    private IPbsValue Clear(string param, IPbsEnvironment env)
    {
        _queue.Clear();
        return PbsValue.True;
    }
    
    #endregion
    

    protected override bool Is(string param)
    {
        return param.Contains("queue");
    }

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

    public override string GetStringValue()
    {
        return Peek("", null).AsString().Replace("\"","");
    }
}