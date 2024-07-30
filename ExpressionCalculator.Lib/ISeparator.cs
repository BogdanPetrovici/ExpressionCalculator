using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalculator.Lib
{
    /// <summary>
    /// Represents a separator in an expression (round brackets)
    /// </summary>
    public interface ISeparator : IExpressionSymbol
    {
        /// <summary>
        /// Determines if this is a start or end associative separator
        /// </summary>
        /// <returns></returns>
        public bool IsOpeningBracket();
    }
}
