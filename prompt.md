### AI Prompt: Base-23 Calculator in C# (.NET 8) with Robust Unit Testing

You are an expert C# developer with a strong understanding of number systems, arithmetic operations, and modern .NET unit testing practices. Your task is to guide me through the process of building a base-23 calculator using .NET 8, emphasizing a robust testing strategy with NUnit, Fluent Assertions, and NSubstitute.

I need a command-line application that can perform basic arithmetic operations (addition, subtraction, multiplication, and division) on numbers represented in base 23.

---

#### Core Requirements:

1.  **Base 23 Representation:**
    * **Digit Mapping:** Provide a clear and consistent mapping for digits 0-22. A common and practical approach is:
        * `0-9` for the first ten digits (0-9).
        * `A-M` for digits 10-22 (where A=10, B=11, ..., M=22). Explain why this mapping is chosen (e.g., readability, avoiding ambiguity with other characters).
    * **Conversion (Base 23 String to Decimal):** Detail the algorithm and C# implementation for converting a base-23 string (e.g., "1M", "G5") into its decimal (base 10) `long` or `BigInteger` equivalent for internal calculations. Discuss handling case-insensitivity.
    * **Conversion (Decimal to Base 23 String):** Detail the algorithm and C# implementation for converting a decimal `long` or `BigInteger` result back into its base-23 string representation. Handle negative numbers correctly if subtraction allows them.

2.  **Arithmetic Operations:**
    * **Addition:** Implement addition for two base-23 numbers.
    * **Subtraction:** Implement subtraction for two base-23 numbers. Clearly define behavior for negative results (e.g., represent with a leading '-' or disallow).
    * **Multiplication:** Implement multiplication for two base-23 numbers.
    * **Division:** Implement division for two base-23 numbers. Handle division by zero by throwing an appropriate exception (`DivideByZeroException`). Discuss how to represent remainders; for simplicity, initially focus on integer division, but consider how a remainder might be represented (e.g., as a second return value or a specific format).

3.  **Input/Output:**
    * The command-line application should offer an interactive loop where users can input two base-23 numbers and an operator (e.g., `+`, `-`, `*`, `/`).
    * Output the result, and any remainder in the case of division, in base 23.
    * Provide clear prompts and result messages.

4.  **Error Handling:**
    * **Invalid Base-23 Input:** Validate input strings to ensure they contain only valid base-23 characters from your defined mapping. Throw a `FormatException` or a custom `InvalidBase23NumberException`.
    * **Invalid Operator:** Handle unsupported operators gracefully.
    * **Division by Zero:** Explicitly catch `DivideByZeroException` and provide a user-friendly message.
    * **Overflow:** While `long` might suffice for many cases, discuss the potential need for `BigInteger` for very large numbers to prevent overflow, especially during multiplication.

---

#### Architectural Guidance:

* **`Base23Number` Class (or struct):** Design a class or struct (prefer struct if immutable and small, class if mutable or large/complex) to encapsulate a base-23 number. This class should:
    * Store the number internally as a `long` or `BigInteger` (decimal equivalent).
    * Provide static `Parse` and `TryParse` methods to convert from a base-23 string.
    * Override `ToString()` to convert back to a base-23 string.
    * Overload arithmetic operators (`+`, `-`, `*`, `/`) to allow natural syntax for calculations.
    * Consider implementing `IEquatable<T>` and overriding `GetHashCode()` for proper comparison.
* **`Base23Converter` (Static Helper Class):** Consider a static utility class specifically for the core conversion logic (`ToDecimal` and `FromDecimal`) if you prefer to keep the `Base23Number` class lighter.
* **`Calculator` Class:** A central `Calculator` class that orchestrates the operations, taking `Base23Number` objects and performing the arithmetic. This will be the primary class for unit testing.
* **Modularity:** Emphasize separation of concerns (e.g., input parsing logic separate from core arithmetic logic).

---

#### Testing Strategy (NUnit, Fluent Assertions, NSubstitute):

1.  **Project Setup:**
    * Create a separate .NET 8 unit test project (e.g., `Base23Calculator.Tests`).
    * Add NuGet packages: `NUnit`, `NUnit3TestAdapter`, `Microsoft.NET.Test.Sdk`, `FluentAssertions`, `NSubstitute`.
2.  **`Base23Number` Tests:**
    * **Conversion Tests:**
        * `[Test]` methods for `Parse` (valid inputs, invalid characters, empty string, null string).
        * `[Test]` methods for `ToString` (decimal to base-23, edge cases like 0, single digit, multiple digits, large numbers).
        * Use `FluentAssertions` for clear assertions (e.g., `result.Should().Be(...)`, `Action action = () => { /* code */ }; action.Should().Throw<ExceptionType>().WithMessage(...)`).
    * **Operator Overload Tests:**
        * `[Test]` methods for `+`, `-`, `*`, `/` operator overloads on `Base23Number` instances, ensuring they return correct `Base23Number` results.
        * Cover positive, negative, zero, and larger numbers.
3.  **`Calculator` Tests:**
    * **Parametrized Tests (`[TestCase]`):** Use NUnit's `[TestCase]` to test various arithmetic operations with different inputs (e.g., `[TestCase("1", "1", "+", "2")]`).
    * **Edge Cases:** Test division by zero, subtraction resulting in negative numbers (if supported), large numbers.
    * **Mocking (NSubstitute - if applicable):** While core `Base23Number` arithmetic might not require mocking, consider scenarios where the `Calculator` might interact with external services (e.g., a logger, an input parser service). Illustrate how `NSubstitute` would be used to mock such dependencies to isolate the `Calculator`'s logic. For example, if you had an `IInputParser` interface, you could mock it to return specific base-23 strings without relying on actual console input during tests.
    * **Error Handling Tests:**
        * Test that `FormatException` is thrown for invalid base-23 strings passed to `Parse`.
        * Test that `DivideByZeroException` is thrown when dividing by "0" (base 23).
        * Use `Should().Throw<TException>()` from Fluent Assertions.

---

#### Step-by-Step Implementation Plan:

1.  **Project Setup:**
    * Create `Base23Calculator` (Console App) and `Base23Calculator.Tests` (NUnit Project) solutions in .NET 8.
    * Add necessary NuGet packages (`NUnit`, `NUnit3TestAdapter`, `Microsoft.NET.Test.Sdk`, `FluentAssertions`, `NSubstitute`) to the test project.
    * Add a project reference from `Base23Calculator.Tests` to `Base23Calculator`.
2.  **`Base23Number` Class/Struct:**
    * Define the character-to-value and value-to-character mapping (e.g., using a `Dictionary<char, int>` and `char[]`).
    * Implement `Base23Number.Parse(string base23String)` and `Base23Number.TryParse(string base23String, out Base23Number result)`.
    * Implement `Base23Number.ToString()`.
    * Implement operator overloads (`+`, `-`, `*`, `/`).
    * Implement `IEquatable<Base23Number>`.
3.  **Unit Tests for `Base23Number`:**
    * Write comprehensive tests for `Parse`, `ToString`, and all overloaded operators in the `Base23Calculator.Tests` project.
    * Use `FluentAssertions` for readable assertions.
4.  **`Calculator` Class:**
    * Create a `Calculator` class with methods like `Add(Base23Number a, Base23Number b)`, `Subtract`, `Multiply`, `Divide`.
    * These methods will internally convert `Base23Number` to decimal, perform `long` or `BigInteger` arithmetic, and convert the result back to `Base23Number`.
5.  **Unit Tests for `Calculator`:**
    * Write tests for the `Calculator` methods, using `[TestCase]` for varied inputs.
    * Demonstrate mocking with `NSubstitute` if the `Calculator` has any dependencies.
6.  **Main Application Logic (Console App):**
    * Implement the interactive command-line loop in `Program.cs`.
    * Use `Base23Number.Parse` to convert user input.
    * Call `Calculator` methods to perform operations.
    * Use `try-catch` blocks for robust error handling and user-friendly messages.

---

#### Example Scenarios (for testing and understanding):

* `10 (base 23) + 1 (base 23) = 11 (base 23)`
* `M (base 23) + 1 (base 23) = 10 (base 23)` (where M = 22)
* `10 (base 23) * 2 (base 23) = 20 (base 23)`
* `1M (base 23) / 2 (base 23) = G (base 23) with remainder 1 (base 23)` (1M = 45 decimal; 45 / 2 = 22 remainder 1; 22 decimal = M base 23)
* `100 (base 23) - 1 (base 23) = MM (base 23)` (100 base 23 = 529 decimal; 529 - 1 = 528 decimal; 528 in base 23 is MM)
* `InvalidBase23Input` (e.g., "1X", "A@") should throw.
* `Division by Zero` (e.g., "10" / "0") should throw.