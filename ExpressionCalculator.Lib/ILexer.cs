namespace ExpressionCalculator.Lib
{
    public interface ILexer
    {
        public IEnumerable<string> GetTokens();
    }
}