using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalculator.Lib
{
    /// <summary>
    /// Handles semantic analysis of an arithmetic expression in postfixed notation and computes the result
    /// </summary>
    public class ExpressionComputer : IExpressionComputer
    {
        private Queue<IExpressionSymbol> _postfixedExpression;
        public ExpressionComputer(Queue<IExpressionSymbol> postfixedExpression)
        {
            if (postfixedExpression == null)
            {
                throw new ArgumentNullException("Invalid postfixed expression");
            }

            _postfixedExpression = postfixedExpression;
        }

        /// <summary>
        /// Computes the result of the arithmetic expression
        /// </summary>
        /// <returns>The result of the computation</returns>
        /// <exception cref="InvalidOperationException">Throws exception in case of missing operands (in operations with cardinality of 2) or division by zero</exception>
        public long Compute()
        {
            Stack<long> operandStack = new Stack<long>();
            while (_postfixedExpression.Count > 0)
            {
                var expressionSymbol = _postfixedExpression.Dequeue();
                if (expressionSymbol is IOperator)
                {
                    if (operandStack.Count == 0)
                    {
                        throw new InvalidOperationException("Missing operand");
                    }

                    var secondOperand = operandStack.Pop();

                    if (operandStack.Count == 0)
                    {
                        throw new InvalidOperationException("Missing operand");
                    }

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
                        case "^":
                            operandStack.Push((long)Math.Pow(firstOperand, secondOperand));
                            break;
                    }
                }
                else
                {
                    operandStack.Push((expressionSymbol as IOperand).Value);
                }
            }

            if (operandStack.Count > 1)
            {
                throw new InvalidOperationException();
            }

            return operandStack.Pop();
        }
    }
}
