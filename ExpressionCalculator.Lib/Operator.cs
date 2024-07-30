using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalculator.Lib
{
    /// <summary>
    /// Class representing the operator in an arithmetic expression. Holds an internal priority used for comparing operators.
    /// </summary>
    public class Operator : IOperator
    {
        private readonly static string _allowedOperators = "+-*/^";
        private string _operator;
        private int _priority;

        public Operator(string c)
        {
            if (!_allowedOperators.Contains(c))
            {
                throw new ArgumentException("Invalid operator");
            }

            _operator = c;
            switch (_operator)
            {
                case "+":
                case "-":
                    _priority = 1;
                    break;
                case "*":
                    _priority = 2;
                    break;
                case "/":
                    _priority = 3;
                    break;
                case "^":
                    _priority = 4;
                    break;
            }
        }

        /// <summary>
        /// Operator priority can have values 1,2,3,4,5
        /// </summary>
        public int Priority { get { return _priority; } }

        /// <summary>
        /// Checks whether the first operator has a lower priority than the second
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>True if the left operator has a lower priority. False otherwise.</returns>
        public static bool operator <(Operator left, Operator right)
        {
            return left.Priority < right.Priority;
        }

        /// <summary>
        /// Checks whether the first operator has a higher priority than the second
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>True if the left operator has a higher priority. False otherwise.</returns>
        public static bool operator >(Operator left, Operator right)
        {
            return left.Priority > right.Priority;
        }

        /// <summary>
        /// Checks whether the left operator has a priority higher than or equal to the right one
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>True if the left operator has a priority higher than or equal to the right one. False otherwise.</returns>
        public static bool operator >=(Operator left, Operator right)
        {
            return left.Priority >= right.Priority;
        }

        /// <summary>
        /// Checks whether the left operator has a priority lower than or equal to the right one
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>Truse if the left operator has a priority lower than or equal to the right one. False otherwise.</returns>
        public static bool operator <=(Operator left, Operator right)
        {
            return left.Priority >= right.Priority;
        }

        public override string ToString() { return _operator.ToString(); }

        public static bool IsOperator(string c) { return _allowedOperators.Contains(c); }
    }
}
