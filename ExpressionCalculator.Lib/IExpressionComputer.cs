using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalculator.Lib
{
    /// <summary>
    /// Handles semantic analysis of an arithmetic expression and computes the result
    /// </summary>
    public interface IExpressionComputer
    {
        /// <summary>
        /// Computes the result of the arithmetic expression
        /// </summary>
        /// <returns>The integer result of the arithmetic expression</returns>
        public long Compute();
    }
}
