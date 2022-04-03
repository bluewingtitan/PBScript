# Comparing Operators

### ==

**==** compares two values with each other (both in type AND value).

Example: `"string value" == "string value" // => true` `"string value" == 899213.155 // => false` `"5" == 5 // => false`

### !=

**!=** compares two values each other (both in type AND value), but is true if the values are UNequal to each other.

Example: `"string value" != "string value" // => false` `"string value" != 899213.155 // => true` `"5" != 5 // => true`

### < and <=

**< / <=** compare two values with each other and are true if the left value is smaller (**<**) or smaller/equal (**<=**) two the right value. Both values have to be a number.

Example: `5 < 5 // => false` `5 <= 5 // => true`

### > and >=

**> / >=** compare two values with each other and are true if the left value is bigger (**>**) or bigger/equal (**>=**) two the right value. Both values have to be a number.

Example: `10 > 5 // => true` `5 >= 5 // => true`

# Mathematical Operators

### Standard Operators

All the basic mathematical operators (**+ , - , \* , /**) work how you would expect them to.

You can use `+` to link together strings with other strings or any other value type:
`"A string " + 10 // => "A string 10"`

### Negative Numbers

In the way how PBScript works, negative numbers are not possible in the standard prefix way ( -10 ). You got two different options: 

`~10` (Tilde as prefix, the built-in way)

`(0-10)` (substract from 0, the mathematical way).

### Modulo

Many would assume this to be a basic operator, but I want to give it it's own section. You may use `x%y` to get the remainder of the division `x/y`. (`11%2` will be 1 for example.)

# Value Modifying Operators

### Direct Assignment

Use `=` to assign a new value to a variable: 

```
var x = 10
x = 5
// x will be 5.
```

### Compound Assignment

Use `=` together with any operator up in the Mathematical Operators Section to add the result of (variable [operator] value) to the variable.

```
var x = 5
x *= 5
// x will be 25.


var string = "A string, "
string += "another string. Number: "
string += 17
// string will be "A string, another string. Number: 17"
```

### Increment/Decrement Operator

Use `++` or `--` to directly increment/decrement a number variable by 1.

```
var x = 0
x++ // x will be 1
x++ // x will be 2

x-- // x will be 1
x -- // x will be 0

while (x < 10)
    x++
    // Do stuff
end

// This while-loop will run 10 times!
```

# Logic Operators

### and

**&&** links two logical terms together and only results in true if both of those are true too. 

Example: `true && true // => true` `true && false // => false`

### or

**||** links two logical terms together and results in true if at least one of those is true.

Example: `true || false // => true` `false || false // => false`

**&&** has priority over **||**, so `true && false || false && true` will first evaluate to `false || true` and then to `true` (Vs. `true && false && true` and then `false`).

### not

**!** negates a boolean value.

Example: `!("string" == "string) // => false` `!false // => true`

**!** has priority over **&&, ||**, so `!false && false` will be evaluated as `(!false) && false // => true && false => false`) instead of `!(false && false) // => !false => true `

### Brackets

**( and )** can be used (besides for calling functions) to change to order of evaluation for logical or mathematical terms:

`2+2*3 // Will be 8, because * has priority over +.` `(2+2)*3 // Will be 12, because the term in the brackets has priority over the one outside.`x = 1x = 1
