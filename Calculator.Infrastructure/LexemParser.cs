using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Infrastructure
{
   public class LexemParser : ILexemParser
   {
      public SymbolType GetSymbolType(string expression, int number)
      {
         if (number > expression.Length)
            throw new ArgumentException("Incorrect number {0}. Has to be less than {1}".F(number, expression.Length));

         var symbol = expression[number];
         if (Char.IsDigit(symbol))
            return SymbolType.number;

         if (Char.IsWhiteSpace(symbol))
            return SymbolType.whitespace;

         switch (symbol)
         {
            case '.':
            case ',':
               return SymbolType.number;

            case '+': return SymbolType.operationAddition;
            case '*': return SymbolType.operationMultiplication;
            case '-': return SymbolType.operationSubtraction;
            case '/': return SymbolType.operationDivision;

            default:
               throw new ArgumentException("Unexpected symbol {0}".F(symbol));
         }
      }

      public enum SymbolType
      {
         none,
         number,
         operationAddition,
         operationSubtraction,
         operationMultiplication,
         operationDivision,
         whitespace,
      }

      protected Lexem MakeLexemOperation(SymbolType symbolType)
      {
         switch (symbolType)
         {
            case SymbolType.operationAddition:
               return new Lexem(OperationEnum.Addition);

            case SymbolType.operationSubtraction:
               return new Lexem(OperationEnum.Subtraction);

            case SymbolType.operationMultiplication:
               return new Lexem(OperationEnum.Multiplication);

            case SymbolType.operationDivision:
               return new Lexem(OperationEnum.Division);

            default:
               throw new NotImplementedException("Unexpected symbol type: {0}".F(symbolType.ToString()));
         }
      }

      public IEnumerable<Lexem> Parse(string expression)
      {
         var lexems = new List<Lexem>(expression.Length);
         if (expression.Length == 0)
            return lexems;

         var current = 0;
         var currentLexem = SymbolType.none;
         do
         {
            currentLexem = GetSymbolType(expression, current);

            switch (currentLexem)
            {
               case SymbolType.whitespace:
                  break;

               case SymbolType.operationAddition:
               case SymbolType.operationDivision:
               case SymbolType.operationMultiplication:
               case SymbolType.operationSubtraction:
                  lexems.Add(MakeLexemOperation(currentLexem));
                  break;

               case SymbolType.number:
                  var next = current + 1;
                  while (next < expression.Length && GetSymbolType(expression, next) == SymbolType.number)
                     next++;

                  if (!double.TryParse(expression.Substring(current, next - current), 
                     NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out var doubleValue))
                     throw new ArgumentException("Incorrect symbols from {0} to {1}".F(current, next - 1));

                  lexems.Add(new Lexem(doubleValue));
                  current = next;
                  break;

               default:
                  throw new NotImplementedException("Unexpected symbol type: {0}".F(currentLexem.ToString()));
            }

            current++;

         } while (current < expression.Length);

         return lexems;
      }
   }
}
