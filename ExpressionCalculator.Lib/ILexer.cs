namespace ExpressionCalculator.Lib
{
    public interface ILexer
    {
        public IEnumerable<IExpressionSymbol> GetTokens();
    }
}