using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalculator.Lib
{
    /// <summary>
    /// Represents an integer operand in arithmetic operations
    /// </summary>
    public class Operand : IOperand
    {
        private double _value;

        public Operand(string operandText)
        {
            double value;
            if (!double.TryParse(operandText, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                throw new ArgumentException(string.Format("Invalid operand: {0}", operandText));
            }

            _value = value;
        }

        public double Value { get { return _value; } }

        public static bool operator >(Operand left, Operand right)
        {
            return left > right;
        }

        public static bool operator <(Operand left, Operand right)
        {
            return left < right;
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
