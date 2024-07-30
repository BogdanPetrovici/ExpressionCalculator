using ExpressionCalculator.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalculator.Test
{
    public class ArithmeticExpressionLexerTests
    {
        [Test]
        public void Lexer_InputValidExpression_ReturnsTokens()
        {
            ILexer lexer = new ArithmeticExpressionLexer("12+45/5-5*10/(2+8)");
            var tokens = lexer.GetTokens();
            var tokenEnumerator = tokens.GetEnumerator();
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo("12"));
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo("+"));
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo("45"));
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo("/"));
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo("5"));
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo("-"));
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo("5"));
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo("*"));
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo("10"));
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo("/"));
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo("("));
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo("2"));
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo("+"));
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo("8"));
            tokenEnumerator.MoveNext();
            Assert.That(tokenEnumerator.Current.ToString(), Is.EqualTo(")"));
            Assert.That(tokenEnumerator.MoveNext(), Is.False);
        }

        [Test]
        public void Lexer_InputEmptyExpression_ReturnsEmptyList()
        {
            ILexer lexer = new ArithmeticExpressionLexer("");
            var tokens = lexer.GetTokens();
            Assert.That(tokens.Count, Is.EqualTo(0));
        }

        [Test]
        public void Lexer_InputInvalidExpression_ThrowsException()
        {
            ILexer lexer = new ArithmeticExpressionLexer("5+a*4");
            Assert.Throws<InvalidOperationException>(() => lexer.GetTokens());

            lexer = new ArithmeticExpressionLexer("5 *4");
            Assert.Throws<InvalidOperationException>(() => lexer.GetTokens());
        }
    }
}
