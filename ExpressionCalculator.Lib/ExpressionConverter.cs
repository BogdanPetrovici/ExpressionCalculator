using System.Text;
using System.Text.RegularExpressions;

namespace ExpressionCalculator.Lib
{
    public class ExpressionConverter : IExpressionConverter
    {
        private string _expression;
        private readonly string _allowedOperators = "+-*";

        public ExpressionConverter(string expression)
        {
            Regex inputValidation = new Regex("^([0-9\\+\\-\\*])+$");
            var match = inputValidation.Match(expression);
            if (!match.Success)
            {
                throw new ArgumentException("Expression must contain only digits, spaces and addition/substraction operators");
            }

            _expression = expression;
        }

        public long Compute()
        {
            var postfixedExpression = ConvertToQueue();
            if (postfixedExpression == null)
            {
                throw new InvalidOperationException("An unexpected error occured");
            }

            Stack<long> operandStack = new Stack<long>();
            long operand;
            while (postfixedExpression.Count > 0)
            {
                var expressionOperand = postfixedExpression.Dequeue();
                if (_allowedOperators.Contains(expressionOperand))
                {
                    var secondOperand = operandStack.Pop();
                    var firstOperand = operandStack.Pop();
                    switch (expressionOperand)
                    {
                        case "+": operandStack.Push(firstOperand + secondOperand); break;
                        case "-": operandStack.Push(firstOperand - secondOperand); break;
                        case "*": operandStack.Push(firstOperand * secondOperand); break;
                    }
                }
                else
                {
                    if (!long.TryParse(expressionOperand, out operand))
                    {
                        throw new InvalidCastException("Operand is not a valid long integer");
                    }

                    operandStack.Push(operand);
                }
            }

            return operandStack.Pop();
        }

        public string Convert()
        {
            var postfixedExpression = ConvertToQueue();
            StringBuilder result = new StringBuilder();
            while (postfixedExpression.Count > 0)
            {
                result.Append(postfixedExpression.Dequeue() + " ");
            }

            return result.ToString();
        }

        /// <summary>
        /// Checks if the first operator is of higher priority than the second one
        /// </summary>
        /// <param name="operator1">First operator - character representing either addition, substraction or multiplication</param>
        /// <param name="operator2">Second operator - character representing either addition, substraction or multiplication</param>
        /// <returns>True if the first operator has a higher priority. Otherwise, false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws ArgumentOutOfRangeException if either argument is not a character representing addition, substraction or multiplication.</exception>
        private bool IsHigherPriority(char operator1, char operator2)
        {
            if (!_allowedOperators.Contains(operator1) || !_allowedOperators.Contains(operator2))
            {
                throw new ArgumentOutOfRangeException("Operator not supported");
            }

            if (operator2 == '*') { return false; }
            else { return true; }
        }

        public Queue<string> ConvertToQueue()
        {
            Queue<string> postfixedExpression = new Queue<string>();
            StringBuilder operandBuilder = new StringBuilder();
            Stack<char> operatorBuffer = new Stack<char>();

            foreach (char c in _expression)
            {
                if (Char.IsDigit(c))
                {
                    operandBuilder.Append(c);
                }
                else
                {
                    if (operandBuilder.Length > 0)
                    {
                        postfixedExpression.Enqueue(operandBuilder.ToString());
                        operandBuilder.Clear();
                        if (operatorBuffer.Count > 0)
                        {
                            // Only add operator to the expression if the next one isn't higher priority (i.e. defer computation if last operand is involved in higher priority operation)
                            if (IsHigherPriority(operatorBuffer.Peek(), c))
                            {
                                while(operatorBuffer.Count > 0)
                                {
                                    postfixedExpression.Enqueue(operatorBuffer.Pop().ToString());
                                }
                            }
                        }
                    }

                    operatorBuffer.Push(c);
                }
            }

            if (operandBuilder.Length == 0)
            {
                throw new InvalidOperationException("Missing operand");
            }

            postfixedExpression.Enqueue(operandBuilder.ToString());

            if (operatorBuffer.Count == 0)
            {
                throw new InvalidOperationException("Missing operator");
            }

            while (operatorBuffer.Count > 0)
            {
                postfixedExpression.Enqueue(operatorBuffer.Pop().ToString());
            }

            return postfixedExpression;
        }
    }
}