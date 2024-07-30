namespace ExpressionCalculator.Lib
{
    public interface IParser
    {
        public Queue<IExpressionSymbol> Parse();
        public Queue<IExpressionSymbol> Parse(IEnumerator<IExpressionSymbol> tokenEnumerator);
    }
}