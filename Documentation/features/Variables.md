# Variables

Variables are storages for values.

Variable names have to start with a letter and may include uppercase and lowercase letters, numbers and underscores.

### Declaration: `var x = value` or `var x set value`

Value may be a fixed value (like `true`, `"string!"` or `420.69`) , another variable or a action (like `aVariable`, `random number`, `$var` or `aStackObject peek`) 

### Assign new values: `x = newValue` or `x set newValue`

Works just like decleration, just without the variable keyword.

`x is [number;string;boolean;null;unsupported;]`

Evaluates to true/false depending if the Value Type of x is the given type.

Also available as isnot (inverted results.)

`x += aValue` or `x add aValue`

Adds aValue to the current value of x. aValue has to evaluate to the the same value-type as x.

### Only available if the Valuetype of x is Number:

`x -= aNumericValue` or `x remove aNumericValue` or `x rmv aNumericValue`

Removes aNumericValue from the current value of x.

`x *= aNumericValue` or `x multiply aNumericValue` or `x ply aNumericValue`

Sets the value of x to it's current value multiplied by aNumericValue.

`x /= aNumericValue` or `x divide aNumericValue` or `x div aNumericValue`

Sets the value of x to it's current value divided by aNumericValue.

`x ++` or `x up`

Adds 1 to the value of x. (Also available if ValueType of x is null or undefined, treating the current value as 0 and updating the type to Number).

`x --` or `x down`

Removes 1 from the value of x. (Also available if ValueType of x is null or undefined, treating the current value as 0 and updating the type to Number).

## Possible Value Types

#### `null`

Null equals no value at all. This variable was assigned an empty value.

#### `boolean`

Variables with the type Boolean either store `true` or `false`

`string`

Variables with the type String store a string of characters.

`number`

Variables with the type Number store a numeric value. The value is stored in a double internally.

#### `unsupported`

Unsupported mostly behaves like null. It's a sign that something went wrong internally and mostly exists to help developers of extra repositories and features debug their code a bit better, as it shows that "something" was assigned somewhere, just not the thing they expected.

# x = 1
