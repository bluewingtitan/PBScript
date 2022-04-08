# FuitIL

FruitIL is an intermediate language for pb-script, consisting out of multiple basic operations.

This part of the documentation is targeted towards both the operation-definitions, but also the expected behaviour of the executing "VM" to make it easy to implement custom VMs for PBScript, but also to create "PBScript-Compatible" languages that also compile to FruitIL.

This file (for now) is a giant monolith of text, I might split it up, i might not.

# 1. The VM

The VM consists out of two parts: The runtime, executing the FruitIL-Code, and the Environment, storing and managing all the available objects and making them requestable by the runtime.

There is no definition towards RAM, Adress-spaces, ..., as PBScript (and thus FruitIL) does not care about stuff like this.

## 1.1 Scopes

All objects are bound to the scope (managed by the environment) they were created in. Scopes are simply nested in each other (=> a function call may acess objects defined in the scope it was called in, but not the other way around, as the functions scope will be closed once the function was fully executed).

The VM is a stack-machine with one stack per nested scope (always executing against the stack of the currently active scope). This feature is used extensively for features like if/elseif/else-statements or loops (=> control flow).

## 1.2 Pre-Execution

The to-be-executed FruitIL-Code will be analyzed beforehand, saving the positions of all lables (lbl <id>) for later use.

# 2. FruitIL-Operators

## 2.1 Value-Related

* psh <value> (push)
  
  * pushes <value> onto the stack.
  
  * value may be 
    
    * numeric (using . as decimal point, - as negative-number prefix)
    
    * boolean (true/false written out)
    
    * string ("...")

* def <name>
  
  * defines a variable in the environment under the name <name>, initialized as null

* pvr <name> (pushvar)
  
  * get's variable known as <name> from the environment and pushes it onto the stack
  
  * if variable is not defined, this will push null onto the stack.
  
  * if the variable exists in a scope above the current scope, this variable will be hidden until the newly defined variable goes out of scope.
  
  * if the variable exists in the same scope as this, it will be overwritten.

* act <name> (action)
  
  * executes the action with the name <name> on top of the top value of the value stack. May consume values from the stack.

* lck (lock)
  
  * pushes a special value onto the stack, that will be ignored by operators, but that signals the maximum point to which a function-call will consume stack-values.

* pop
  
  * pops the top value from the stack (simply discards it)

* clr (clear)
  
  * clears the stack of all values

* opr <operator> (operator)
  
  * applies the operator <operator> onto the stack, popping the used values and pushing the result onto the stack in the process
  
  * Example:
    
    * Stack: 7, 10
    
    * Operator: -
    
    * pops the 10, pops the 7. executes 7-10, puts -3 onto the stack.

## 2.2 Flow Control

* lbl <id>
  
  * defines a label with <id>
  
  * Won't have any effect during execution, is a market to be picked out and saved in the pre-execution routine.

* jmp <id>
  
  * jumps to the label <id>

* jif <idif> (jump-if)
  
  * reads the top value of the stack (only peeks!)
    
    * value is boolean and true: Jump to <idif>
    
    * value is boolean + false, or no value, or not boolean: Continues execution (=> next operations will act like a else-statement, only executing in the case of false).

* cll <id> (call)
  
  * jumps to label <id>, but also saves the current position onto the call-stack, making a return to this position possible from the called label.
  
  * Does not close the current scope, as it's ment to be returned to.

* bck (back)
  
  * jumps to the last position saved on the call-stack
  
  * **closes the current scope**, as returning to it won't be possible (as it's jumping back, out of it)

* yld <value> (yield)
  
  * jumps to the last position saved on the call-stack (popping it), puts <value> onto the value stack of the current scope AFTER closing the current one
  
  * **closes the current scope**, as returning to it won't be possible (as it's jumping back, out of it)
- yvr <name> (yield-var)
  
  - jumps to the last position saved on the call-stack (popping it), puts value of the varialbe <name> onto the value stack of the current scope AFTER closing the current one
  
  - **closes the current scope**, as returning to it won't be possible (as it's jumping back, out of it)

## 2.3 Scope-Control

(Besides the scope-control defined in 2.2)

* scp (scope)
  
  * Adds a new scope nested in the current scope.

* usc (unscope)
  
  * closes the current scope. If you are not sure about how to use this, using clear will be more save!

## 2.4 Library-Management

* rqs <name>
  
  * requests a packet from a Repository
  
  * Example:
    
    * "rqst pbs/dict" will request the packet pbs/dict, containing the dictionary-object, and adds it to the environment as variable.
