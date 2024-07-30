using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpressionCalculator.Lib
{
    /// <summary>
    /// Processes a string representing an arithmetic expression as a list of tokens.
    /// Handles invalid character errors
    /// </summary>
    public class ArithmeticExpressionLexer : ILexer
    {
        private readonly string _expression;

        public ArithmeticExpressionLexer(string expression)
        {
            _expression = expression;
        }

        public IEnumerable<IExpressionSymbol> GetTokens()
        {
            StringBuilder operand = new StringBuilder();
            List<IExpressionSymbol> tokens = new List<IExpressionSymbol>();

            foreach (char c in _expression)
            {
                if (Operator.IsOperator(c.ToString()))
                {
                    if(operand.Length > 0)
                    {
                        tokens.Add(new Operand(operand.ToString()));
                        operand.Clear();
                    }

                    tokens.Add(new Operator(c.ToString()));
                }
                else if (Separator.IsSeparator(c.ToString()))
                {
                    if (operand.Length > 0)
                    {
                        tokens.Add(new Operand(operand.ToString()));
                        operand.Clear();
                    }

                    tokens.Add(new Separator(c.ToString()));
                }
                else if (Char.IsDigit(c) || c == '.')
                {
                    operand.Append(c);
                }
                else
                {
                    throw new InvalidOperationException(string.Format("Invalid character encountered: {0}", c));
                }
            }

            if (operand.Length > 0)
            {
                tokens.Add(new Operand(operand.ToString()));
            }

            return tokens;
        }
    }
}
