using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalculator.Lib
{
    public interface ISeparator : IExpressionSymbol
    {
        public bool IsOpeningBracket();
    }
}
