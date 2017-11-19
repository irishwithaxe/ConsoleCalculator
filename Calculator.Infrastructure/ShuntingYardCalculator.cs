using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Infrastructure
{
   public class ShuntingYardCalculator : ICalculator
   {
      public IEnumerable<Lexem> TranslateToPostfixNotation(IEnumerable<Lexem> expression)
      {
         var list = new List<Lexem>();
         var stack = new Stack<Lexem>();

         foreach (var current in expression)
         {
            if (!current.IsOperation)
            {
               list.Add(current);
               continue;
            }

            while (stack.Any() &&
               stack.First().OperationPriority >= current.OperationPriority)
            {
               list.Add(stack.Pop());
            }

            stack.Push(current);
         }

         while (stack.Any())
            list.Add(stack.Pop());

         return list;
      }

      public double Calculate(IEnumerable<Lexem> lexems)
      {
         var postfixNotation = TranslateToPostfixNotation(lexems);

         var stack = new Stack<Lexem>();
         Lexem left, right;

         void _pop(Lexem op)
         {
            if (stack.Count < 2)
               throw new ArgumentException("Argument for operation '{0}' is missing".F(op.ToString()));

            right = stack.Pop();
            left = stack.Pop();
         }

         foreach (var current in postfixNotation)
         {
            if (current.IsOperation)
            {
               _pop(current);
               stack.Push(current.Calculate(left, right));
            }
            else
            {
               stack.Push(current);
            }
         }

         if (stack.Count != 1)
            throw new ArgumentException("Incorrect expression.");

         return stack.Pop().Value;
      }
   }
}
