using ExpressionCalculator.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalculator.Test
{
    public class ExpressionComputerTests
    {
        [Test]
        public void Compute_FloatingPointExpression_ComputesFloatingPointResult()
        {
            var lexer = new ArithmeticExpressionLexer("(5.2-1.7)*2.5");
            var converter = new ReversePolishNotationParser(lexer);
            var computer = new ExpressionComputer(converter.Parse());
            var result = computer.Compute();
            Assert.That(result, Is.EqualTo(8.75));
        }

        [Test]
        public void Compute_ExpressionWithLargeResult_ThrowsException()
        {
            var lexer = new ArithmeticExpressionLexer("1.7*10^309");
            var converter = new ReversePolishNotationParser(lexer);
            var computer = new ExpressionComputer(converter.Parse());
            var exception = Assert.Throws<InvalidOperationException>(() => computer.Compute());
            Assert.That(exception.Message, Is.EqualTo("Result is too large"));

            lexer = new ArithmeticExpressionLexer("10^308*10^308");
            converter = new ReversePolishNotationParser(lexer);
            computer = new ExpressionComputer(converter.Parse());
            exception = Assert.Throws<InvalidOperationException>(() => computer.Compute());
            Assert.That(exception.Message, Is.EqualTo("Result is too large"));
        }
    }
}
