using System;
using System.Numerics;

namespace Base23Calculator
{
    /// <summary>
    /// Provides arithmetic operations for Base23Number values.
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Adds two Base23Numbers and returns the result.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <returns>The sum of a and b.</returns>
        public Base23Number Add(Base23Number a, Base23Number b)
        {
            return a + b;
        }

        /// <summary>
        /// Subtracts the second Base23Number from the first and returns the result.
        /// </summary>
        /// <param name="a">The number to subtract from.</param>
        /// <param name="b">The number to subtract.</param>
        /// <returns>The difference a - b.</returns>
        public Base23Number Subtract(Base23Number a, Base23Number b)
        {
            return a - b;
        }

        /// <summary>
        /// Multiplies two Base23Numbers and returns the result.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <returns>The product of a and b.</returns>
        public Base23Number Multiply(Base23Number a, Base23Number b)
        {
            return a * b;
        }

        /// <summary>
        /// Divides the first Base23Number by the second and returns the result.
        /// </summary>
        /// <param name="a">The dividend.</param>
        /// <param name="b">The divisor.</param>
        /// <returns>The quotient a / b.</returns>
        /// <exception cref="DivideByZeroException">Thrown when b is zero.</exception>
        public Base23Number Divide(Base23Number a, Base23Number b)
        {
            return a / b;
        }

        /// <summary>
        /// Divides the first Base23Number by the second and returns both the quotient and remainder.
        /// </summary>
        /// <param name="a">The dividend.</param>
        /// <param name="b">The divisor.</param>
        /// <param name="remainder">When this method returns, contains the remainder of the division.</param>
        /// <returns>The quotient a / b.</returns>
        /// <exception cref="DivideByZeroException">Thrown when b is zero.</exception>
        public Base23Number DivideWithRemainder(Base23Number a, Base23Number b, out Base23Number remainder)
        {
            return Base23Number.DivRem(a, b, out remainder);
        }
    }
}