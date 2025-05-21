using FluentAssertions;
using NSubstitute;

namespace Base23Calculator.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [TestCase("10", "1", "11")]
        [TestCase("M", "1", "10")]
        [TestCase("MM", "1", "100")]
        public void Add_TwoBase23Numbers_ReturnsCorrectSum(string a, string b, string expected)
        {
            // Arrange
            Base23Number first = Base23Number.Parse(a);
            Base23Number second = Base23Number.Parse(b);
            Base23Number expectedResult = Base23Number.Parse(expected);

            // Act
            Base23Number result = _calculator.Add(first, second);

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestCase("10", "1", "M")]
        [TestCase("100", "1", "MM")]
        [TestCase("M", "A", "C")]
        public void Subtract_TwoBase23Numbers_ReturnsCorrectDifference(string a, string b, string expected)
        {
            // Arrange
            Base23Number first = Base23Number.Parse(a);
            Base23Number second = Base23Number.Parse(b);
            Base23Number expectedResult = Base23Number.Parse(expected);

            // Act
            Base23Number result = _calculator.Subtract(first, second);

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestCase("10", "2", "20")]
        [TestCase("10", "10", "100")]
        [TestCase("M", "2", "1L")]
        public void Multiply_TwoBase23Numbers_ReturnsCorrectProduct(string a, string b, string expected)
        {
            // Arrange
            Base23Number first = Base23Number.Parse(a);
            Base23Number second = Base23Number.Parse(b);
            Base23Number expectedResult = Base23Number.Parse(expected);

            // Act
            Base23Number result = _calculator.Multiply(first, second);

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestCase("10", "2", "B")]
        [TestCase("100", "10", "10")]
        [TestCase("1M", "3", "F")]
        public void Divide_TwoBase23Numbers_ReturnsCorrectQuotient(string a, string b, string expected)
        {
            // Arrange
            Base23Number first = Base23Number.Parse(a);
            Base23Number second = Base23Number.Parse(b);
            Base23Number expectedResult = Base23Number.Parse(expected);

            // Act
            Base23Number result = _calculator.Divide(first, second);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Test]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            Base23Number first = Base23Number.Parse("10");
            Base23Number zero = Base23Number.Parse("0");

            // Act & Assert
            FluentActions.Invoking(() => _calculator.Divide(first, zero))
                .Should().Throw<DivideByZeroException>();
        }

        [TestCase("1M", "2", "M", "1")]
        [TestCase("100", "3", "7F", "1")]
        [TestCase("MM", "M", "11", "0")]
        public void DivideWithRemainder_TwoBase23Numbers_ReturnsCorrectQuotientAndRemainder(
            string a, string b, string expectedQuotient, string expectedRemainder)
        {
            // Arrange
            Base23Number first = Base23Number.Parse(a);
            Base23Number second = Base23Number.Parse(b);
            Base23Number expectedQuotientValue = Base23Number.Parse(expectedQuotient);
            Base23Number expectedRemainderValue = Base23Number.Parse(expectedRemainder);

            // Act
            Base23Number quotient = _calculator.DivideWithRemainder(first, second, out Base23Number remainder);

            // Assert
            quotient.Should().Be(expectedQuotientValue);
            remainder.Should().Be(expectedRemainderValue);
        }
    }
}