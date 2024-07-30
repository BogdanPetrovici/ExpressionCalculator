using ExpressionCalculator.Lib;
using System.Text.RegularExpressions;

namespace ExpressionCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input expression: ");
            string userInput = Console.ReadLine();

            try
            {
                ILexer lexer = new ArithmeticExpressionLexer(userInput);
                var parser = new ReversePolishNotationParser(lexer);
                var postfixedExpression = parser.Parse();
                var computer = new ExpressionComputer(postfixedExpression);
                Console.WriteLine("Result: {0}", computer.Compute());
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: ", ex);
            }
        }
    }
}