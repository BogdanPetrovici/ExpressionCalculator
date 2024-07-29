using System.Text;
using System.Text.RegularExpressions;

namespace ExpressionCalculator.Lib
{
    public class ExpressionConverter : IExpressionConverter
    {
        private string _expression;

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
                if ("+-".Contains(expressionOperand))
                {
                    var secondOperand = operandStack.Pop();
                    var firstOperand = operandStack.Pop();
                    switch (expressionOperand)
                    {
                        case "+": operandStack.Push(firstOperand + secondOperand); break;
                        case "-": operandStack.Push(firstOperand - secondOperand); break;
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

        public Queue<string> ConvertToQueue()
        {
            Queue<string> postfixedExpression = new Queue<string>();
            StringBuilder operandBuilder = new StringBuilder();
            char? operationBuffer = null;

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
                        if (operationBuffer.HasValue)
                        {
                            postfixedExpression.Enqueue(operationBuffer.ToString());
                        }
                    }

                    operationBuffer = c;
                }
            }

            if (operandBuilder.Length == 0)
            {
                throw new InvalidOperationException("Missing operand");
            }

            postfixedExpression.Enqueue(operandBuilder.ToString());

            if (!operationBuffer.HasValue)
            {
                throw new InvalidOperationException("Missing operator");
            }

            postfixedExpression.Enqueue(operationBuffer.ToString());
            return postfixedExpression;
        }
    }
}