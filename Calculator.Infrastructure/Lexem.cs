using System;

namespace Calculator.Infrastructure
{
   public struct Lexem
   {
      private OperationEnum _op;
      private double _value;

      public Lexem(double value)
      {
         _op = OperationEnum.None;
         _value = value;
      }

      public Lexem(OperationEnum operation)
      {
         if (operation == OperationEnum.None)
            throw new ArgumentException("Operation value '{0}' is incorrect.".F(operation.ToString()));

         _op = operation;
         _value = 0.0;
      }

      public double Value => _value;

      public bool IsOperation => _op != OperationEnum.None;

      public byte OperationPriority => (byte)_op;

      public Lexem Calculate(Lexem left, Lexem right)
      {
         if (!IsOperation)
            throw new ArgumentException("Current lexem is not an operation.");
         if (left.IsOperation)
            throw new ArgumentException("Left lexem is not a number.");
         if (right.IsOperation)
            throw new ArgumentException("Right lexem is not a number.");

         var newLexem = new Lexem();
         switch (_op)
         {
            case OperationEnum.Addition:
               newLexem._value = left.Value + right.Value;
               break;

            case OperationEnum.Subtraction:
               newLexem._value = left.Value - right.Value;
               break;

            case OperationEnum.Multiplication:
               newLexem._value = left.Value * right.Value;
               break;

            case OperationEnum.Division:
               newLexem._value = left.Value / right.Value;
               break;

            default:
               throw new NotImplementedException("Operation {0} is not expected.".F(_op.ToString()));
         }

         return newLexem;
      }
   }
}