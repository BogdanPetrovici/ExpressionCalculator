namespace ExpressionCalculator.Lib
{
    public interface IOperator : IExpressionSymbol
    {
        public int Priority { get; }
    }
}