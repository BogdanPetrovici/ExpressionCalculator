namespace ExpressionCalculator.Lib
{
    public interface IParser
    {
        public Queue<IExpressionSymbol> Parse();
    }
}