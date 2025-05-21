# Base23 Calculator

This is a console application that performs arithmetic operations using base-23 number system.

## Features

- Supports addition, subtraction, multiplication, and division of base-23 numbers
- Handles remainders in division operations
- Case-insensitive input
- Comprehensive error handling

## Digit Mapping

- Digits 0-9 represent values 0-9
- Letters A-M represent values 10-22

## Usage

Run the application and enter calculations in the format:

```
<number1> <operator> <number2>
```

For example:
- `10 + 1` (addition)
- `100 - 1` (subtraction)
- `10 * 2` (multiplication)
- `1M / 2` (division)

## Example Calculations

- `10 (base 23) + 1 (base 23) = 11 (base 23)`
- `M (base 23) + 1 (base 23) = 10 (base 23)` (where M = 22)
- `10 (base 23) * 2 (base 23) = 20 (base 23)`
- `1M (base 23) / 2 (base 23) = G (base 23) with remainder 1 (base 23)`
- `100 (base 23) - 1 (base 23) = MM (base 23)`
