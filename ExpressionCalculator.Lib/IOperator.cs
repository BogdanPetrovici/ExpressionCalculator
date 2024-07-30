namespace ExpressionCalculator.Lib
{
    /// <summary>
    /// Represents an operator in an arithmetic expression
    /// </summary>
    public interface IOperator : IExpressionSymbol
    {
        /// <summary>
        /// The operator's execution priority. Higher values mean more priority.
        /// </summary>
        public int Priority { get; }
    }
}