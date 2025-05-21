using System;

namespace Base23Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            Console.WriteLine("========================================");
            Console.WriteLine("Base23 Calculator");
            Console.WriteLine("Digits: 0-9 for values 0-9, A-M for values 10-22");
            Console.WriteLine("Operations: + (add), - (subtract), * (multiply), / (divide)");
            Console.WriteLine("Type 'exit' to quit");
            Console.WriteLine("========================================");

            while (true)
            {
                Console.WriteLine("\nEnter calculation (e.g., 10 + 1 or 1M / 2):");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) || input.ToLower() == "exit")
                {
                    break;
                }

                ProcessInput(input, calculator);
            }
        }

        private static void ProcessInput(string input, Calculator calculator)
        {
            try
            {
                // Parse input
                string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 3)
                {
                    Console.WriteLine("Invalid input format. Please use format: number operator number (e.g., 10 + 1)");
                    return;
                }

                // Parse numbers and operator
                if (!Base23Number.TryParse(parts[0], out Base23Number a))
                {
                    Console.WriteLine($"Invalid first number: {parts[0]}");
                    return;
                }

                if (!Base23Number.TryParse(parts[2], out Base23Number b))
                {
                    Console.WriteLine($"Invalid second number: {parts[2]}");
                    return;
                }

                string op = parts[1];

                // Perform calculation
                Base23Number result;
                switch (op)
                {
                    case "+":
                        result = calculator.Add(a, b);
                        Console.WriteLine($"Result: {result}");
                        break;
                    case "-":
                        result = calculator.Subtract(a, b);
                        Console.WriteLine($"Result: {result}");
                        break;
                    case "*":
                        result = calculator.Multiply(a, b);
                        Console.WriteLine($"Result: {result}");
                        break;
                    case "/":
                        if (op == "/" && b.DecimalValue == 0)
                        {
                            Console.WriteLine("Error: Division by zero is not allowed.");
                            return;
                        }

                        Base23Number remainder;
                        result = calculator.DivideWithRemainder(a, b, out remainder);
                        if (remainder.DecimalValue == 0)
                        {
                            Console.WriteLine($"Result: {result}");
                        }
                        else
                        {
                            Console.WriteLine($"Result: {result} with remainder {remainder}");
                        }
                        break;
                    default:
                        Console.WriteLine($"Unsupported operator: {op}. Please use +, -, *, or /");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
