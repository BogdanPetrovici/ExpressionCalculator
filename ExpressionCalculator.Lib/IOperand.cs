namespace ExpressionCalculator.Lib
{
    public interface IOperand : IExpressionSymbol
    {
        public long Value { get; }
    }
}