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
            var lexer = new ArithmeticExpressionLexer("12+45-56e+8");
            var converter = new ReversePolishNotationParser(lexer);
            Assert.Throws<InvalidOperationException>(() => converter.Parse());
        }

        [Test]
        public void ExpressionConverter_InitializedCorrectly_ReturnsPostfixNotation()
        {
            var lexer = new ArithmeticExpressionLexer("12+45-56+8");
            var converter = new ReversePolishNotationParser(lexer);
            var postfixedExpression = converter.Parse();
            Assert.IsNotNull(postfixedExpression);
            Assert.That(postfixedExpression.Count, Is.EqualTo(7));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("12"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("45"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("+"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("56"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("-"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("8"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("+"));
        }

        [Test]
        public void ExpressionConverter_InputExpressionWithMultiplication_ReturnsPostfixNotation()
        {
            var lexer = new ArithmeticExpressionLexer("12+45-5*10+8");
            var converter = new ReversePolishNotationParser(lexer);
            var postfixedExpression = converter.Parse();
            Assert.IsNotNull(postfixedExpression);
            Assert.That(postfixedExpression.Count, Is.EqualTo(9));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("12"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("45"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("+"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("5"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("10"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("*"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("-"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("8"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("+"));
        }

        [Test]
        public void ExpressionConverter_InputExpressionWithDivision_ReturnsPostfixNotation()
        {
            var lexer = new ArithmeticExpressionLexer("12+45/9-5*10+8");
            var converter = new ReversePolishNotationParser(lexer);
            var postfixedExpression = converter.Parse();
            Assert.IsNotNull(postfixedExpression);
            Assert.That(postfixedExpression.Count, Is.EqualTo(11));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("12"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("45"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("9"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("/"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("+"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("5"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("10"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("*"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("-"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("8"));
            Assert.That(postfixedExpression.Dequeue().ToString(), Is.EqualTo("+"));
        }

        [Test]
        public void ExpressionConverter_MissingOperand_ThrowsException()
        {
            var lexer = new ArithmeticExpressionLexer("12+3++");
            var converter = new ReversePolishNotationParser(lexer);
            Assert.Throws<InvalidOperationException>(() => converter.Parse());
        }

        [Test]
        public void ExpressionConverter_MissingOperator_ThrowsException()
        {
            var lexer = new ArithmeticExpressionLexer("12");
            var converter = new ReversePolishNotationParser(lexer);
            Assert.Throws<InvalidOperationException>(() => converter.Parse());
        }

        [Test]
        public void ExpressionConverter_InitializedCorrectly_ComputesExpression()
        {
            var lexer = new ArithmeticExpressionLexer("12+45-56+8");
            var converter = new ReversePolishNotationParser(lexer);
            var computer = new ExpressionComputer(converter.Parse());
            var result = computer.Compute();
            Assert.That(result, Is.EqualTo(9));
        }

        [Test]
        public void ExpressionConverter_InputExpressionWithMultiplication_ComputesExpression()
        {
            var lexer = new ArithmeticExpressionLexer("12+45-5*10+8");
            var converter = new ReversePolishNotationParser(lexer);
            var computer = new ExpressionComputer(converter.Parse());
            var result = computer.Compute();
            Assert.That(result, Is.EqualTo(15));
        }

        [Test]
        public void ExpressionConverter_InputExpressionWithDivision_ComputesExpression()
        {
            var lexer = new ArithmeticExpressionLexer("12+45/5-5*10+8");
            var converter = new ReversePolishNotationParser(lexer);
            var computer = new ExpressionComputer(converter.Parse());
            var result = computer.Compute();
            Assert.That(result, Is.EqualTo(-21));

            lexer = new ArithmeticExpressionLexer("12+45/5-5*10/2+8");
            converter = new ReversePolishNotationParser(lexer);
            computer = new ExpressionComputer(converter.Parse());
            result = computer.Compute();
            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void ExpressionConverter_InputExpressionWithBrackets_ComputesExpression()
        {
            var lexer = new ArithmeticExpressionLexer("5*(4+2)");
            var converter = new ReversePolishNotationParser(lexer);
            var computer = new ExpressionComputer(converter.Parse());
            var result = computer.Compute();
            Assert.That(result, Is.EqualTo(30));

            lexer = new ArithmeticExpressionLexer("(4+2)*5");
            converter = new ReversePolishNotationParser(lexer);
            computer = new ExpressionComputer(converter.Parse());
            result = computer.Compute();
            Assert.That(result, Is.EqualTo(30));

            lexer = new ArithmeticExpressionLexer("4+2*5");
            converter = new ReversePolishNotationParser(lexer);
            computer = new ExpressionComputer(converter.Parse());
            result = computer.Compute();
            Assert.That(result, Is.EqualTo(14));

            lexer = new ArithmeticExpressionLexer("4+(2*5)");
            converter = new ReversePolishNotationParser(lexer);
            computer = new ExpressionComputer(converter.Parse());
            result = computer.Compute();
            Assert.That(result, Is.EqualTo(14));

            lexer = new ArithmeticExpressionLexer("(4+2-(10+4)/2)*5");
            converter = new ReversePolishNotationParser(lexer);
            computer = new ExpressionComputer(converter.Parse());
            result = computer.Compute();
            Assert.That(result, Is.EqualTo(-5));

            lexer = new ArithmeticExpressionLexer("(4+2-(10+4-2*2)/2)*5");
            converter = new ReversePolishNotationParser(lexer);
            computer = new ExpressionComputer(converter.Parse());
            result = computer.Compute();
            Assert.That(result, Is.EqualTo(5));

            lexer = new ArithmeticExpressionLexer("(4+2-(10+(4-2)*2)/2)*5");
            converter = new ReversePolishNotationParser(lexer);
            computer = new ExpressionComputer(converter.Parse());
            result = computer.Compute();
            Assert.That(result, Is.EqualTo(-5));
        }

        [Test]
        public void ExpressionConverter_MissingOpeningBracket_ComputesExpression()
        {
            var lexer = new ArithmeticExpressionLexer("5*4+2)");
            var converter = new ReversePolishNotationParser(lexer);
            var computer = new ExpressionComputer(converter.Parse());
            var exception = Assert.Throws<InvalidOperationException>(() => computer.Compute());
            Assert.That(exception.Message, Is.EqualTo("Missing opening bracket"));
        }

        [Test]
        public void ExpressionConverter_MissingClosingBracket_ComputesExpression()
        {
            var lexer = new ArithmeticExpressionLexer("5*(4+2");
            var converter = new ReversePolishNotationParser(lexer);
            var computer = new ExpressionComputer(converter.Parse());
            var exception = Assert.Throws<InvalidOperationException>(() => computer.Compute());
            Assert.That(exception.Message, Is.EqualTo("Missing closing bracket"));
        }
    }
}