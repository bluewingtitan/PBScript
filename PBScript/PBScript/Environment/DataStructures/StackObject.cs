using PBScript.Interfaces;

namespace PBScript.Environment.DataStructures;

public class StackObject: ObjectBase
{
    private readonly Stack<PbsValue> _stack = new();

    public StackObject(string objectName)
    {
        ObjectName = objectName;
        
        RegisterTyped("pop", Pop);
        RegisterTyped("peek", Peek);
        RegisterTyped("clear", Clear);
        RegisterTyped("count", Count);
        RegisterTyped("push", Push);
    }

    protected override PbsValue DefaultAction(string command, PbsValue[] param, IPbsEnvironment env)
    {
        return Peek(null, null);
    }

    #region Operations

    private PbsValue Count(PbsValue[] param, IPbsEnvironment env)
    {
        return new PbsValue(_stack.Count);
    }
    
    private PbsValue Pop(PbsValue[] param, IPbsEnvironment env)
    {
        return _stack.Count>0 ? _stack.Pop() : PbsValue.Null;
    }

    private PbsValue Peek(PbsValue[]? param, IPbsEnvironment? env)
    {
        return _stack.Count>0 ? _stack.Peek() : PbsValue.Null;
    }
    
    private PbsValue Push(PbsValue[] value, IPbsEnvironment env)
    {
        if (value.Length < 1)
        {
            return PbsValue.False;
        }

        _stack.Push(value[0]);
        return PbsValue.True;
    }
    
    private PbsValue Clear(PbsValue[] param, IPbsEnvironment env)
    {
        _stack.Clear();
        return PbsValue.True;
    }
    
    #endregion

    public override string GetDocumentation()
    {
        return @"Just a basic stack.
Use 'stack create newStack' to create a new stack with the name 'name'.

Use 'var x = newStack pop' to pop the top element and set it to a variables value
Use 'newStack push x' to add the value of variable x to the stack.
Use 'newStack clear' to clear the stack.
Use 'newStack' or 'newStack peek' to get the top element without removing it.
Use 'newStack count' to get the amount of items in the stack.";
    }

    public override string ObjectName { get; }
}