# Debug Repository

> Creation: v0.1
> 
> Last Update: v0.6.1

Only usable for debug and developement purposes, should not be enabled for production.

## Objects

### debug `request pbs/debug`

 `debug log text` Logs text in a raw manner to default output (does not evaluate text before. "string" + 1 will be logged as "string" + 1 instead of string1).

`debug traceOn` Activates Trace Mode (Prints internal execution details into standard out)

`debug traceOff` Deactivates Trace Mode

---------

### assert

A basic utility for testing code. Checks if certain conditions are met and saves the value for later use after code execution.

Usage always is `assert (mode) value`. The result get's stored internally until the next assert is runned. You can save the result in the key-value store of the assertObject with `assert save (name)`, and acess it inside of the tests with `AssertObject.Results[(name)]`.

##### Modes (`description` condition that has to be met to be true)

`assert true value` value is boolean and true

`assert false value` value is boolean and false

`assert null value` value is null

`assert notnull value` value anything but null or unsupported (Any specific, langauge-internal value-type)
