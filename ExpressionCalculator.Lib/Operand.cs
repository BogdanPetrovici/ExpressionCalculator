using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalculator.Lib
{

    public class Operand : IOperand
    {
        private long _value;

        public Operand(string operandText)
        {
            long value;
            if (!long.TryParse(operandText, out value))
            {
                throw new ArgumentException("Invalid operand");
            }

            _value = value;
        }

        public long Value { get { return _value; } }

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
            return _value.ToString();
        }
    }
}
