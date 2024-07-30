namespace ExpressionCalculator.Lib
{
    /// <summary>
    /// Interface for expression parsing classes. 
    /// Parsers get a list/stream of IExpressionSymbol instances and process them into various data structures more fit for syntactic analysis and execution.
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Rearranges and filters the supplied IExpressionSymbol instances into a Queue
        /// </summary>
        /// <returns>Queue of IExpressionSymbol instances</returns>
        public Queue<IExpressionSymbol> Parse();
    }
}