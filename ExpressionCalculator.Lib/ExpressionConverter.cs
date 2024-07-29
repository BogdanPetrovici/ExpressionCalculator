using System.Text;
using System.Text.RegularExpressions;

namespace ExpressionCalculator.Lib
{
    public class ExpressionConverter : IExpressionConverter
    {
        private ILexer _lexer;

        public ExpressionConverter(ILexer lexer)
        {
            _lexer = lexer;
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
            Stack<Operator> operatorBuffer = new Stack<Operator>();

            var tokens = _lexer.GetTokens();
            foreach (string token in tokens)
            {
                if (Operator.IsOperator(token))
                {
                    Operator nextOperator = new Operator(token);

                    // Only add operator to the expression if the next one isn't higher priority (i.e. defer computation if last operand is involved in higher priority operation)
                    if (operatorBuffer.Count > 0)
                    {
                        if (operatorBuffer.Peek() >= nextOperator || nextOperator.Priority == 5)
                        {
                            while (operatorBuffer.Count > 0 && operatorBuffer.Peek().Priority != 4)
                            {
                                postfixedExpression.Enqueue(operatorBuffer.Pop());
                            }

                            if(operatorBuffer.Count > 0 && operatorBuffer.Peek().Priority == 4 && nextOperator.Priority == 5)
                            {
                                operatorBuffer.Pop();
                            }
                        }
                    }

                    if (nextOperator.Priority != 5)
                    {
                        operatorBuffer.Push(nextOperator);
                    }
                }
                else
                {
                    postfixedExpression.Enqueue(new Operand(token));
                }
            }

            while (operatorBuffer.Count > 0)
            {
                postfixedExpression.Enqueue(operatorBuffer.Pop());
            }

            return postfixedExpression;
        }
    }
}