using PBScript.Interfaces;

namespace PBScript.Environment.DataStructures;

public class StackObject: ObjectBase
{
    private readonly Stack<IPbsValue> Stack = new();

    public StackObject(string objectName)
    {
        ObjectName = objectName;
        
        Register("pop", Pop);
        Register("peek", Peek);
        Register("clear", Clear);
        Register("count", Count);
        RegisterTyped("push", Push);
    }

    protected override IPbsValue DefaultAction(string param)
    {
        return Peek("", null);
    }

    #region Operations

    private IPbsValue Count(string param, IPbsEnvironment env)
    {
        return new PbsValue(Stack.Count);
    }
    
    private IPbsValue Pop(string param, IPbsEnvironment env)
    {
        return Stack.Count>0 ? Stack.Pop() : PbsValue.Null;
    }

    private IPbsValue Peek(string param, IPbsEnvironment? env)
    {
        return Stack.Count>0 ? Stack.Peek() : PbsValue.Null;
    }
    
    private IPbsValue Push(IPbsValue value, IPbsEnvironment env)
    {
        Stack.Push(value);
        return PbsValue.True;
    }
    
    private IPbsValue Clear(string param, IPbsEnvironment env)
    {
        Stack.Clear();
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
    public override string ObjectType => "stack";

    public override string GetStringValue()
    {
        return Peek("", null).AsString();
    }
}