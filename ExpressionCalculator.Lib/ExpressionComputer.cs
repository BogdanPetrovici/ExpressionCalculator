﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalculator.Lib
{
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
