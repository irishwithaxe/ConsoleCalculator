using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Calculator.Infrastructure;
using System.Linq;

namespace UnitTestProject
{
   [TestClass]
   public class ShuntingYardPostfixNotationCalculateTest
   {
      [TestMethod]
      public void TestMethod1()
      {
         // 7 - 2 * 3
         var lexemList = new List<Lexem>() {
            new Lexem(7),
            new Lexem(OperationEnum.Subtraction),
            new Lexem(2),
            new Lexem(OperationEnum.Multiplication),
            new Lexem(3),
         };

         var calc = new ShuntingYardCalculator();

         var result = calc.Calculate(lexemList);

         Assert.AreEqual(1, result);
      }

      [TestMethod]
      public void TestMethod2()
      {
         // 7 - 2 * 3 + 1 + 1 * 1 + 2 / 1
         var lexemList = new List<Lexem>() {
            new Lexem(7),
            new Lexem(OperationEnum.Subtraction),
            new Lexem(2),
            new Lexem(OperationEnum.Multiplication),
            new Lexem(3),
            new Lexem(OperationEnum.Addition),
            new Lexem(1),
            new Lexem(OperationEnum.Addition),
            new Lexem(1),
            new Lexem(OperationEnum.Multiplication),
            new Lexem(1),
            new Lexem(OperationEnum.Addition),
            new Lexem(2),
            new Lexem(OperationEnum.Division),
            new Lexem(1),
         };

         var calc = new ShuntingYardCalculator();

         var result = calc.Calculate(lexemList);

         Assert.AreEqual(5, result);
      }
   }
}
