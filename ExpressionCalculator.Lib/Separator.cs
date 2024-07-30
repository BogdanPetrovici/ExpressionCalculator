using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalculator.Lib
{
    public class Separator : ISeparator
    {
        private readonly static string _allowedSeparators = "()";
        private string _separator;

        public Separator(string c)
        {
            if (!_allowedSeparators.Contains(c))
            {
                throw new ArgumentException("Invalid separator");
            }

            _separator = c;
        }

        public bool IsOpeningBracket()
        {
            return _separator == "(";
        }

        public static bool IsSeparator(string c)
        {
            return _allowedSeparators.Contains(c);
        }

        public override string ToString() { return _separator; }
    }
}
