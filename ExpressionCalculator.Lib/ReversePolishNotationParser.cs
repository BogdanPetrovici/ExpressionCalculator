using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;

namespace ExpressionCalculator.Lib
{
    public class ReversePolishNotationParser : IParser
    {
        private ILexer _lexer;

        public ReversePolishNotationParser(ILexer lexer)
        {
            _lexer = lexer;
        }

        public Queue<IExpressionSymbol> Parse(IEnumerator<IExpressionSymbol> tokenEnumerator)
        {
            Queue<IExpressionSymbol> postfixedExpression = new Queue<IExpressionSymbol>();
            Stack<Operator> operatorBuffer = new Stack<Operator>();
            // end of expression
            if (!tokenEnumerator.MoveNext())
            {
                return postfixedExpression;
            }

            do
            {
                IExpressionSymbol token = tokenEnumerator.Current;
                if (token is Separator)
                {
                    var separator = token as Separator;
                    if (separator.IsOpeningBracket())
                    {
                        // if we encounter open bracket, parse subexpression and add it to current expression queue
                        Queue<IExpressionSymbol> postfixedSubexpression = Parse(tokenEnumerator);
                        while (postfixedSubexpression.Count > 0)
                        {
                            postfixedExpression.Enqueue(postfixedSubexpression.Dequeue());
                        }
                    }
                    else
                    {
                        // Add remaining operators from buffer to subexpression
                        while (operatorBuffer.Count > 0)
                        {
                            postfixedExpression.Enqueue(operatorBuffer.Pop());
                        }

                        // if closing bracket, return parsed subexpression
                        return postfixedExpression;
                    }
                }
                else if (token is Operator)
                {
                    // end of subexpression
                    Operator nextOperator = token as Operator;

                    while (operatorBuffer.Count > 0 && operatorBuffer.Peek() >= nextOperator)
                    {
                        postfixedExpression.Enqueue(operatorBuffer.Pop());
                    }

                    // add next operator to the buffer
                    operatorBuffer.Push(nextOperator);
                }
                else if (token is Operand)
                {
                    postfixedExpression.Enqueue(token as Operand);
                }
            } while (tokenEnumerator.MoveNext());

            // Add remaining operators from buffer to expression
            while (operatorBuffer.Count > 0)
            {
                postfixedExpression.Enqueue(operatorBuffer.Pop());
            }

            return postfixedExpression;
        }

        public Queue<IExpressionSymbol> Parse()
        {
            var tokenEnumerator = _lexer.GetTokens().GetEnumerator();
            return Parse(tokenEnumerator);
        }
    }
}