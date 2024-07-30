namespace ExpressionCalculator.Lib
{
    /// <summary>
    /// Interface for expression lexer classes. Gets tokens as IExpressionSymbol instances for further processing.
    /// </summary>
    public interface ILexer
    {
        /// <summary>
        /// Tokenizes expressions as a list of IExpressionSymbol instances
        /// </summary>
        /// <returns>A list of IExpressionSymbol instances</returns>
        public IEnumerable<IExpressionSymbol> GetTokens();
    }
}