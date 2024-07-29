namespace ExpressionCalculator.Lib
{
    public interface IExpressionConverter
    {
        public string Convert();
        public Queue<IExpressionSymbol> ConvertToQueue();
        public long Compute();
    }
}