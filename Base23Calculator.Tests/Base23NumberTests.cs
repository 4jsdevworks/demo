using FluentAssertions;
using System.Numerics;

namespace Base23Calculator.Tests
{
    [TestFixture]
    public class Base23NumberTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("0", 0)]
        [TestCase("1", 1)]
        [TestCase("9", 9)]
        [TestCase("A", 10)]
        [TestCase("M", 22)]
        [TestCase("10", 23)]
        [TestCase("11", 24)]
        [TestCase("1M", 45)]
        [TestCase("MM", 528)]
        [TestCase("100", 529)]
        [TestCase("-1", -1)]
        [TestCase("-M", -22)]
        public void Parse_ValidBase23String_ReturnsExpectedDecimalValue(string base23String, long expected)
        {
            // Act
            Base23Number number = Base23Number.Parse(base23String);

            // Assert
            number.DecimalValue.Should().Be(new BigInteger(expected));
        }

        [TestCase("")]
        [TestCase("X")]
        [TestCase("1X")]
        [TestCase("@")]
        [TestCase("A@")]
        [TestCase("N")]
        public void Parse_InvalidBase23String_ThrowsFormatException(string invalidBase23String)
        {
            // Act & Assert
            FluentActions.Invoking(() => Base23Number.Parse(invalidBase23String))
                .Should().Throw<FormatException>();
        }

        [Test]
        public void Parse_NullString_ThrowsArgumentNullException()
        {
            // Act & Assert
            FluentActions.Invoking(() => Base23Number.Parse(null!))
                .Should().Throw<ArgumentNullException>();
        }

        [TestCase(0, "0")]
        [TestCase(1, "1")]
        [TestCase(9, "9")]
        [TestCase(10, "A")]
        [TestCase(22, "M")]
        [TestCase(23, "10")]
        [TestCase(24, "11")]
        [TestCase(45, "1M")]
        [TestCase(528, "MM")]
        [TestCase(529, "100")]
        [TestCase(-1, "-1")]
        [TestCase(-22, "-M")]
        public void ToString_DecimalValue_ReturnsExpectedBase23String(long decimalValue, string expected)
        {
            // Arrange
            var number = new Base23Number(decimalValue);

            // Act
            string result = number.ToString();

            // Assert
            result.Should().Be(expected);
        }

        [TestCase("10", "1", "11")]
        [TestCase("M", "1", "10")]
        [TestCase("MM", "1", "100")]
        public void Addition_TwoBase23Numbers_ReturnsCorrectSum(string a, string b, string expected)
        {
            // Arrange
            Base23Number first = Base23Number.Parse(a);
            Base23Number second = Base23Number.Parse(b);
            Base23Number expectedResult = Base23Number.Parse(expected);

            // Act
            Base23Number result = first + second;

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestCase("10", "1", "M")]
        [TestCase("100", "1", "MM")]
        [TestCase("M", "A", "C")]
        public void Subtraction_TwoBase23Numbers_ReturnsCorrectDifference(string a, string b, string expected)
        {
            // Arrange
            Base23Number first = Base23Number.Parse(a);
            Base23Number second = Base23Number.Parse(b);
            Base23Number expectedResult = Base23Number.Parse(expected);

            // Act
            Base23Number result = first - second;

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestCase("10", "2", "20")]
        [TestCase("10", "10", "100")]
        [TestCase("M", "2", "1L")]
        public void Multiplication_TwoBase23Numbers_ReturnsCorrectProduct(string a, string b, string expected)
        {
            // Arrange
            Base23Number first = Base23Number.Parse(a);
            Base23Number second = Base23Number.Parse(b);
            Base23Number expectedResult = Base23Number.Parse(expected);

            // Act
            Base23Number result = first * second;

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestCase("10", "2", "B")]
        [TestCase("100", "10", "10")]
        [TestCase("1M", "3", "F")]
        public void Division_TwoBase23Numbers_ReturnsCorrectQuotient(string a, string b, string expected)
        {
            // Arrange
            Base23Number first = Base23Number.Parse(a);
            Base23Number second = Base23Number.Parse(b);
            Base23Number expectedResult = Base23Number.Parse(expected);

            // Act
            Base23Number result = first / second;

            // Assert
            result.Should().Be(expectedResult);
        }

        [Test]
        public void Division_ByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            Base23Number first = Base23Number.Parse("10");
            Base23Number zero = Base23Number.Parse("0");

            // Act & Assert
            FluentActions.Invoking(() => first / zero)
                .Should().Throw<DivideByZeroException>();
        }

        [TestCase("1M", "2", "M", "1")]
        [TestCase("100", "3", "7F", "1")]
        [TestCase("MM", "M", "11", "0")]
        public void DivRem_TwoBase23Numbers_ReturnsCorrectQuotientAndRemainder(
            string a, string b, string expectedQuotient, string expectedRemainder)
        {
            // Arrange
            Base23Number first = Base23Number.Parse(a);
            Base23Number second = Base23Number.Parse(b);
            Base23Number expectedQuotientValue = Base23Number.Parse(expectedQuotient);
            Base23Number expectedRemainderValue = Base23Number.Parse(expectedRemainder);

            // Act
            Base23Number quotient = Base23Number.DivRem(first, second, out Base23Number remainder);

            // Assert
            quotient.Should().Be(expectedQuotientValue);
            remainder.Should().Be(expectedRemainderValue);
        }

        [TestCase("10", "10", true)]
        [TestCase("A", "a", true)]  // Case insensitive
        [TestCase("MM", "MM", true)]
        [TestCase("10", "11", false)]
        [TestCase("A", "B", false)]
        public void Equals_ComparingBase23Numbers_ReturnsExpectedResult(string a, string b, bool expectedEqual)
        {
            // Arrange
            Base23Number first = Base23Number.Parse(a);
            Base23Number second = Base23Number.Parse(b);

            // Act & Assert
            if (expectedEqual)
            {
                first.Should().Be(second);
                (first == second).Should().BeTrue();
                (first != second).Should().BeFalse();
            }
            else
            {
                first.Should().NotBe(second);
                (first == second).Should().BeFalse();
                (first != second).Should().BeTrue();
            }
        }
    }
}