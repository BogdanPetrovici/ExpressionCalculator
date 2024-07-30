using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;

namespace ExpressionCalculator.Lib
{
    /// <summary>
    /// Class for parsing an infixed arithmetic expression supplied in the form of a list of IExpressionSymbol instances 
    /// into postfixed notation (Reverse Polish Notation) stored in a queue of IExpressionSymbol instances.
    /// Handles priority imposed by round brackets and removes them from the end result.
    /// </summary>
    public class ReversePolishNotationParser : IParser
    {
        private ILexer _lexer;

        public ReversePolishNotationParser(ILexer lexer)
        {
            _lexer = lexer;
        }

        /// <summary>
        /// Recursive method for iterating through an IExpressionSymbol token enumerator and generating postfixed expressions.
        /// </summary>
        /// <param name="tokenEnumerator">Enumerator supplying IExpressionSymbol instances (tokens) in their original infixed order</param>
        /// <param name="separatorBuffer">Stack keeping track of open round brackets between recursive calls</param>
        /// <returns>A queue of IExpressionSymbol instances representing the postfixed notation of the current subexpression defined by round brackets (or the entire expression)</returns>
        /// <exception cref="InvalidOperationException">If a closing bracket is detected without there being an open bracket in the buffer, an exception is thrown</exception>
        private Queue<IExpressionSymbol> Parse(IEnumerator<IExpressionSymbol> tokenEnumerator, Stack<Separator> separatorBuffer)
        {
            Queue<IExpressionSymbol> postfixedExpression = new Queue<IExpressionSymbol>();
            Stack<Operator> operatorBuffer = new Stack<Operator>();

            while (tokenEnumerator.MoveNext())
            {
                IExpressionSymbol token = tokenEnumerator.Current;
                if (token is Separator)
                {
                    var separator = token as Separator;
                    if (separator.IsOpeningBracket())
                    {
                        separatorBuffer.Push(separator);

                        // if we encounter open bracket, parse subexpression and add it to current expression queue
                        Queue<IExpressionSymbol> postfixedSubexpression = Parse(tokenEnumerator, separatorBuffer);
                        while (postfixedSubexpression.Count > 0)
                        {
                            postfixedExpression.Enqueue(postfixedSubexpression.Dequeue());
                        }
                    }
                    else
                    {
                        if(separatorBuffer.Count == 0)
                        {
                            throw new InvalidOperationException("Missing open bracket");
                        }

                        separatorBuffer.Pop();

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
            }

            // Add remaining operators from buffer to expression
            while (operatorBuffer.Count > 0)
            {
                postfixedExpression.Enqueue(operatorBuffer.Pop());
            }

            return postfixedExpression;
        }

        /// <summary>
        /// Parses the output of the lexer into a postfixed notation
        /// </summary>
        /// <returns>A queue representing the postfixed notation of the infixed arithmetic expression supplied by the user</returns>
        /// <exception cref="InvalidOperationException">If after processing there are unhandled open round brackets, an exception is thrown</exception>
        public Queue<IExpressionSymbol> Parse()
        {
            var tokenEnumerator = _lexer.GetTokens().GetEnumerator();
            Stack<Separator> separatorBuffer = new Stack<Separator>();

            var postfixedExpression =  Parse(tokenEnumerator, separatorBuffer);

            // check if we had any open brackets that weren't closed
            if (separatorBuffer.Count > 0)
            {
                throw new InvalidOperationException("Missing closed bracket");
            }

            return postfixedExpression;
        }
    }
}