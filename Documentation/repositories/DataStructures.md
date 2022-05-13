# Data Structures Repository

> Creation: v0.6
> 
> Last Update: v0.7.0 = 1
> 
> Code-Coverage: 100%

Grants access to stacks and queues.

## Objects

### stack `request pbs/datastructures:stack` (Creator Object)

`stack.create()` Creates and attaches a new stack-object.

### stack-object `var newStack = stack.create()`

`newStack.pop()` Pops and returns the top value from the stack.

`newStack.peek()` Returns the top value from the stack without removing it.

`newStack.push(value)` Pushes $value on top of the stack

`newStack.clear()` Clears the stack (Removes all elements)

`newStack.count()` Returns the amount of items inside of the stack.

---------

### queue `request pbs/datastructures:queue` (Creator Object)

`queue.create()`Creates and attaches a new queue-object

### queue-object `var newQueue = queue.create()`

`newQueue.dequeue()` Dequeues and returns the front value in the queue.

`newQueue.peek()` Returns the front value from the queue without removing it.

`newQueue.enqueue(value)` Enqueues $value at the back of the queue.

`newQueue.clear()` Clears the queue (Removes all elements)

`newQueue.count()` Returns the amount of items inside of the queue.
