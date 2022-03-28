# Data Structures Repository

> Creation: v0.6
> 
> Last Update: v0.7x = 1
> 
> Code-Coverage: 100%

Grants access to stacks and queues.

## Objects

### stack `request pbs/stack` (Creator Object)

`stack.create(name)` Creates and attaches a new stack-object under the name $name (using 'newStack' as example).

### stack-object `stack.create("newStack")`

`newStack.pop()` Pops and returns the top value from the stack.

`newStack.peek()` Returns the top value from the stack without removing it.

`newStack.push(value)` Pushes $value on top of the stack

`newStack.clear()` Clears the stack (Removes all elements)

`newStack.count()` Returns the amount of items inside of the stack.

---------

### queue `request pbs/queue` (Creator Object)

`queue.create(name)`Creates and attaches a new queue-object under the name $name (using 'newQueue' as example).

### queue-object `queue.create("newQueue")`

`newQueue.dequeue()` Dequeues and returns the front value in the queue.

`newQueue.peek()` Returns the front value from the queue without removing it.

`newQueue.enqueue(value)` Enqueues $value at the back of the queue.

`newQueue.clear()` Clears the queue (Removes all elements)

`newQueue.count()` Returns the amount of items inside of the queue.
