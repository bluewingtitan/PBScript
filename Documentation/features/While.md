# While

**while** opens up a conditional block. It loops over the code inside of the block as long as the given condition still evaluates to true after the execution. It will evaluate the condition before executing the code, meaning the code inside will never get executed if the condition is false in the beginning.

```
var x = 10

// the following loop will run 10 times, as x will be equal to zero
// after 10 runs, so the condition will no longer
// evaluate to true.
while x > 0
    x down // decreases x by 1
end

while x > 0
    // Code here will never run, because x isn't bigger than 0
    // when first evaluating the condition.
end
```

While-Loops are closed by an end. See [End](End.md) for more.

See [Logic Operators](LogicOperators.md) for more information on writing conditions.
