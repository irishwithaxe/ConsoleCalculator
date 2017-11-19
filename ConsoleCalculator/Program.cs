using Calculator.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Welcome to calculator. Input expression and press enter.");
         Console.WriteLine("For exit input empty expression.");

         try
         {
            var parser = new LexemParser();
            var calc = new ShuntingYardCalculator();

            while (ProcessExpression(parser, calc)) ;

         }
         catch (Exception ex)
         {
            Console.WriteLine("Exception occurred: {0}".F(ex.Message));
         }
      }

      private static bool ProcessExpression(LexemParser parser, ShuntingYardCalculator calc)
      {
         try
         {
            Console.Write("expression: ");
            var expression = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(expression))
               return false;

            var lexems = parser.Parse(expression);
            var result = calc.Calculate(lexems);
            Console.WriteLine("Result: {0}".F(result));
         }
         catch (Exception ex)
         {
            Console.WriteLine("Exception occurred: {0}".F(ex.Message));
         }

         return true;
      }
   }
}
