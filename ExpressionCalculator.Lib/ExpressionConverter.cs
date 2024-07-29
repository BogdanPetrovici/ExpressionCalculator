using System.Text;
using System.Text.RegularExpressions;

namespace ExpressionCalculator.Lib
{
    public class ExpressionConverter : IExpressionConverter
    {
        private string _expression;

        public ExpressionConverter(string expression)
        {
            Regex inputValidation = new Regex("^([0-9\\+\\-\\*\\/])+$");
            var match = inputValidation.Match(expression);
            if (!match.Success)
            {
                throw new ArgumentException("Expression must contain only digits and addition/substraction/multiplication operators");
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
            while (postfixedExpression.Count > 0)
            {
                var expressionSymbol = postfixedExpression.Dequeue();
                if (expressionSymbol is IOperator)
                {
                    var secondOperand = operandStack.Pop();
                    var firstOperand = operandStack.Pop();
                    switch (expressionSymbol.ToString())
                    {
                        case "+": operandStack.Push(firstOperand + secondOperand); break;
                        case "-": operandStack.Push(firstOperand - secondOperand); break;
                        case "*": operandStack.Push(firstOperand * secondOperand); break;
                        case "/":
                            if (secondOperand == 0) { throw new InvalidOperationException("Division by zero."); }
                            operandStack.Push(firstOperand / secondOperand);
                            break;
                    }
                }
                else
                {
                    operandStack.Push((expressionSymbol as IOperand).Value);
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

        public Queue<IExpressionSymbol> ConvertToQueue()
        {
            Queue<IExpressionSymbol> postfixedExpression = new Queue<IExpressionSymbol>();
            StringBuilder operandBuilder = new StringBuilder();
            Stack<Operator> operatorBuffer = new Stack<Operator>();

            foreach (char c in _expression)
            {
                if (Char.IsDigit(c))
                {
                    operandBuilder.Append(c);
                }
                else
                {
                    Operator nextOperator = new Operator(c);
                    if (operandBuilder.Length > 0)
                    {
                        postfixedExpression.Enqueue(new Operand(operandBuilder.ToString()));
                        operandBuilder.Clear();
                        if (operatorBuffer.Count > 0)
                        {
                            // Only add operator to the expression if the next one isn't higher priority (i.e. defer computation if last operand is involved in higher priority operation)
                            if (operatorBuffer.Peek() >= nextOperator)
                            {
                                while (operatorBuffer.Count > 0)
                                {
                                    postfixedExpression.Enqueue(operatorBuffer.Pop());
                                }
                            }
                        }
                    }

                    operatorBuffer.Push(nextOperator);
                }
            }

            if (operandBuilder.Length == 0)
            {
                throw new InvalidOperationException("Missing operand");
            }

            postfixedExpression.Enqueue(new Operand(operandBuilder.ToString()));

            if (operatorBuffer.Count == 0)
            {
                throw new InvalidOperationException("Missing operator");
            }

            while (operatorBuffer.Count > 0)
            {
                postfixedExpression.Enqueue(operatorBuffer.Pop());
            }

            return postfixedExpression;
        }
    }
}