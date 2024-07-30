using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalculator.Lib
{
    /// <summary>
    /// Class representing the round brackets in an arithmetic expression
    /// </summary>
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

        /// <summary>
        /// Checks whether this separator is an open round bracket
        /// </summary>
        /// <returns>True, if this is an open bracket. False, otherwise</returns>
        public bool IsOpeningBracket()
        {
            return _separator == "(";
        }

        /// <summary>
        /// Determines whether the supplied string is an acceptable separator
        /// </summary>
        /// <param name="c">String token from the infixed expression</param>
        /// <returns>True, if the string is a valid separator. False, otherwise</returns>
        public static bool IsSeparator(string c)
        {
            return _allowedSeparators.Contains(c);
        }

        public override string ToString() { return _separator; }
    }
}
