using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpressionCalculator.Lib
{
    public class ArithmeticExpressionLexer : ILexer
    {
        private readonly string _expression;

        public ArithmeticExpressionLexer(string expression)
        {
            _expression = expression;
        }

        public IEnumerable<string> GetTokens()
        {
            StringBuilder operand = new StringBuilder();
            List<string> tokens = new List<string>();

            foreach (char c in _expression)
            {
                if (Operator.IsOperator(c.ToString()))
                {
                    if(operand.Length > 0)
                    {
                        tokens.Add(operand.ToString());
                        operand.Clear();
                    }

                    tokens.Add(c.ToString());
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
                tokens.Add(operand.ToString());
            }

            return tokens;
        }
    }
}
