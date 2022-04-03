# If

If opens a conditional block. It takes in a condition and only executes the following block if the input evaluates to `true`.

```
var x = true
if x
    // code here will be executed, because x is true.
end

if not x
    // code here won't be executed, because (not x) is false.
end
```

See [Operators](Operators.md) for more information on writing conditions.

# Else

**else** closes an if or elseif block (in place of an end), and only get's executed if none of the blocks before (that are part of this specific if/elseif/else-structure) weren't executed.

```
var x = false
if x
    // code here won't get executed, because x is false
else
    // code here will get executed, because the if-block wasn't executed.
end
```

# Elseif

**elseif** opens up a conditional block, closes an if or elseif block (in place of an end or else) and only get's executed if the blocks before (that are part of this specific if/elseif/else-structure) weren't executed and the condition evaluates to true.

```
var x = false
var y = true
if x
    // code here won't get executed, because x is false
elseif y
    // code here will get executed, because the if-block wasn't executed
    // and the condition (y) is true.
else
    // code here won't get executed because one of the blocks before ran.
end
```

# End

See [End](End.md).
