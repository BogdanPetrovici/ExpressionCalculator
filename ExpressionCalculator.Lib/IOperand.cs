namespace ExpressionCalculator.Lib
{
    /// <summary>
    /// Represents an operand in arithmetic expressions
    /// </summary>
    public interface IOperand : IExpressionSymbol
    {
        /// <summary>
        /// Numeric value of an expression operand
        /// </summary>
        public long Value { get; }
    }
}