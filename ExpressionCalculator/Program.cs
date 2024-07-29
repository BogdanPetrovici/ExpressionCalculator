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
                ExpressionConverter expression = new ExpressionConverter(lexer);
                Console.WriteLine("Result: {0}", expression.Compute());
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: ", ex);
            }
        }
    }
}