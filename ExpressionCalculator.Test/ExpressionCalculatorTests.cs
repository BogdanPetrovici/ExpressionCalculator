using ExpressionCalculator.Lib;

namespace ExpressionCalculator.Test
{
    public class ExpressionCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ExpressionConverter_InitializedWithInvalidCharacters_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new ExpressionConverter("12+45-56e+8"));
        }

        [Test]
        public void ExpressionConverter_InitializedCorrectly_ReturnsPostfixNotation()
        {
            var converter = new ExpressionConverter("12+45-56+8");
            var postfixedExpression = converter.ConvertToQueue();
            Assert.IsNotNull(postfixedExpression);
            Assert.That(postfixedExpression.Count, Is.EqualTo(7));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("12"));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("45"));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("+"));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("56"));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("-"));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("8"));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("+"));

            Assert.That(converter.Convert(), Is.EqualTo("12 45 + 56 - 8 + "));
        }

        [Test]
        public void ExpressionConverter_InputExpressionWithMultiplication_ReturnsPostfixNotation()
        {
            var converter = new ExpressionConverter("12+45-5*10+8");
            var postfixedExpression = converter.ConvertToQueue();
            Assert.IsNotNull(postfixedExpression);
            Assert.That(postfixedExpression.Count, Is.EqualTo(9));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("12"));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("45"));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("+"));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("5"));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("10"));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("*"));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("-"));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("8"));
            Assert.That(postfixedExpression.Dequeue, Is.EqualTo("+"));

            Assert.That(converter.Convert(), Is.EqualTo("12 45 + 5 10 * - 8 + "));
        }

        [Test]
        public void ExpressionConverter_MissingOperand_ThrowsException()
        {
            var converter = new ExpressionConverter("12+3++");
            Assert.Throws<InvalidOperationException>(() => converter.Convert());
        }

        [Test]
        public void ExpressionConverter_MissingOperator_ThrowsException()
        {
            var converter = new ExpressionConverter("12");
            Assert.Throws<InvalidOperationException>(() => converter.Convert());
        }

        [Test]
        public void ExpressionConverter_InitializedCorrectly_ComputesExpression()
        {
            var converter = new ExpressionConverter("12+45-56+8");
            var result = converter.Compute();
            Assert.That(result, Is.EqualTo(9));
        }

        [Test]
        public void ExpressionConverter_InputExpressionWithMultiplication_ComputesExpression()
        {
            var converter = new ExpressionConverter("12+45-5*10+8");
            var result = converter.Compute();
            Assert.That(result, Is.EqualTo(15));
        }
    }
}