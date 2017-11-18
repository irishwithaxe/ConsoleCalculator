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
               stack.Last().IsOperation &&
               stack.Last().OperationPriority >= current.OperationPriority)
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

         void _pop()
         {
            if (stack.Any())
               right = stack.Pop();
            else
               throw new ArgumentException("Incorrect expression.");

            if (stack.Any())
               left = stack.Pop();
            else
               throw new ArgumentException("Incorrect expression.");
         }

         foreach (var current in postfixNotation)
         {
            if (current.IsOperation)
            {
               _pop();
               stack.Push(current.Calculate(left, right));
            }
            else
            {
               stack.Push(current);
            }
         }

         if(stack.Count != 1)
            throw new ArgumentException("Incorrect expression.");

         return stack.Pop().Value;
      }
   }
}
