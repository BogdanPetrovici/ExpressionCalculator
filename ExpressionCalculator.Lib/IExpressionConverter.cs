namespace ExpressionCalculator.Lib
{
    public interface IExpressionConverter
    {
        public string Convert();
        public Queue<string> ConvertToQueue();
        public long Compute();
    }
}