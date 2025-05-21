using System;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace Base23Calculator
{
    /// <summary>
    /// Represents a number in base 23 with support for arithmetic operations.
    /// </summary>
    public readonly struct Base23Number : IEquatable<Base23Number>
    {
        private static readonly char[] DigitToChar = "0123456789ABCDEFGHIJKLM".ToCharArray();
        private static readonly Dictionary<char, int> CharToDigit = new();

        private readonly BigInteger _decimalValue;
        private const int Base = 23;

        static Base23Number()
        {
            // Initialize the dictionary for character to digit conversion
            for (int i = 0; i < DigitToChar.Length; i++)
            {
                CharToDigit[char.ToUpper(DigitToChar[i])] = i;
                CharToDigit[char.ToLower(DigitToChar[i])] = i;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Base23Number"/> struct with a decimal value.
        /// </summary>
        /// <param name="decimalValue">The decimal value.</param>
        public Base23Number(BigInteger decimalValue)
        {
            _decimalValue = decimalValue;
        }

        /// <summary>
        /// Gets the internal decimal representation of this Base23Number.
        /// </summary>
        public BigInteger DecimalValue => _decimalValue;

        /// <summary>
        /// Converts the base-23 string representation to its decimal equivalent.
        /// </summary>
        /// <param name="base23String">The base-23 string to convert.</param>
        /// <returns>A Base23Number representing the converted value.</returns>
        /// <exception cref="FormatException">Thrown when the input string is not a valid base-23 number.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the input string is null.</exception>
        public static Base23Number Parse(string base23String)
        {
            if (base23String == null)
                throw new ArgumentNullException(nameof(base23String));

            if (string.IsNullOrEmpty(base23String))
                throw new FormatException("Input string was not in a correct format.");

            bool isNegative = base23String[0] == '-';
            int startIndex = isNegative ? 1 : 0;

            if (base23String.Length <= startIndex)
                throw new FormatException("Input string was not in a correct format.");

            BigInteger result = 0;
            for (int i = startIndex; i < base23String.Length; i++)
            {
                char c = base23String[i];
                if (!CharToDigit.TryGetValue(c, out int digitValue))
                    throw new FormatException($"Invalid character '{c}' in base-23 string.");

                result = result * Base + digitValue;
            }

            return new Base23Number(isNegative ? -result : result);
        }

        /// <summary>
        /// Tries to convert the base-23 string representation to its decimal equivalent.
        /// </summary>
        /// <param name="base23String">The base-23 string to convert.</param>
        /// <param name="result">When this method returns, contains the converted value if the conversion succeeded.</param>
        /// <returns>true if the conversion succeeded; otherwise, false.</returns>
        public static bool TryParse(string base23String, out Base23Number result)
        {
            result = default;

            if (string.IsNullOrEmpty(base23String))
                return false;

            bool isNegative = base23String[0] == '-';
            int startIndex = isNegative ? 1 : 0;

            if (base23String.Length <= startIndex)
                return false;

            BigInteger decimalValue = 0;
            for (int i = startIndex; i < base23String.Length; i++)
            {
                char c = base23String[i];
                if (!CharToDigit.TryGetValue(c, out int digitValue))
                    return false;

                decimalValue = decimalValue * Base + digitValue;
            }

            result = new Base23Number(isNegative ? -decimalValue : decimalValue);
            return true;
        }

        /// <summary>
        /// Converts the decimal value to its base-23 string representation.
        /// </summary>
        /// <returns>A string representing the base-23 number.</returns>
        public override string ToString()
        {
            if (_decimalValue == 0)
                return "0";

            BigInteger absValue = BigInteger.Abs(_decimalValue);
            StringBuilder result = new();

            while (absValue > 0)
            {
                BigInteger remainder = absValue % Base;
                result.Insert(0, DigitToChar[(int)remainder]);
                absValue /= Base;
            }

            if (_decimalValue < 0)
                result.Insert(0, '-');

            return result.ToString();
        }

        #region Arithmetic Operations

        /// <summary>
        /// Adds two Base23Number values.
        /// </summary>
        public static Base23Number operator +(Base23Number left, Base23Number right)
        {
            return new Base23Number(left._decimalValue + right._decimalValue);
        }

        /// <summary>
        /// Subtracts one Base23Number from another.
        /// </summary>
        public static Base23Number operator -(Base23Number left, Base23Number right)
        {
            return new Base23Number(left._decimalValue - right._decimalValue);
        }

        /// <summary>
        /// Multiplies two Base23Number values.
        /// </summary>
        public static Base23Number operator *(Base23Number left, Base23Number right)
        {
            return new Base23Number(left._decimalValue * right._decimalValue);
        }

        /// <summary>
        /// Divides one Base23Number by another.
        /// </summary>
        /// <exception cref="DivideByZeroException">Thrown when the divisor is zero.</exception>
        public static Base23Number operator /(Base23Number left, Base23Number right)
        {
            if (right._decimalValue == 0)
                throw new DivideByZeroException("Division by zero is not allowed.");

            return new Base23Number(left._decimalValue / right._decimalValue);
        }

        /// <summary>
        /// Gets the remainder from dividing one Base23Number by another.
        /// </summary>
        /// <exception cref="DivideByZeroException">Thrown when the divisor is zero.</exception>
        public static Base23Number operator %(Base23Number left, Base23Number right)
        {
            if (right._decimalValue == 0)
                throw new DivideByZeroException("Division by zero is not allowed.");

            return new Base23Number(left._decimalValue % right._decimalValue);
        }

        #endregion

        #region Equality Comparison

        /// <summary>
        /// Returns a value indicating whether this instance is equal to the specified Base23Number.
        /// </summary>
        public bool Equals(Base23Number other)
        {
            return _decimalValue.Equals(other._decimalValue);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to the specified object.
        /// </summary>
        public override bool Equals(object? obj)
        {
            return obj is Base23Number other && Equals(other);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return _decimalValue.GetHashCode();
        }

        /// <summary>
        /// Returns a value indicating whether two Base23Number instances are equal.
        /// </summary>
        public static bool operator ==(Base23Number left, Base23Number right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Returns a value indicating whether two Base23Number instances are not equal.
        /// </summary>
        public static bool operator !=(Base23Number left, Base23Number right)
        {
            return !left.Equals(right);
        }

        #endregion

        #region Division with Remainder

        /// <summary>
        /// Performs integer division with remainder.
        /// </summary>
        /// <param name="dividend">The dividend (number being divided).</param>
        /// <param name="divisor">The divisor (number dividing the dividend).</param>
        /// <param name="remainder">The remainder after division.</param>
        /// <returns>The quotient of the division.</returns>
        /// <exception cref="DivideByZeroException">Thrown when the divisor is zero.</exception>
        public static Base23Number DivRem(Base23Number dividend, Base23Number divisor, out Base23Number remainder)
        {
            if (divisor._decimalValue == 0)
                throw new DivideByZeroException("Division by zero is not allowed.");

            BigInteger quotient = BigInteger.DivRem(dividend._decimalValue, divisor._decimalValue, out BigInteger rem);
            remainder = new Base23Number(rem);
            return new Base23Number(quotient);
        }

        #endregion
    }
}