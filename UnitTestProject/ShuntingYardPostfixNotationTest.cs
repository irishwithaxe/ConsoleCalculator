using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Calculator.Infrastructure;
using System.Linq;

namespace UnitTestProject
{
   [TestClass]
   public class ShuntingYardPostfixNotationTest
   {
      [TestMethod]
      public void TestMethod1()
      {
         var lexemList = new List<Lexem>() {
            new Lexem(5),
            new Lexem(OperationEnum.Addition),
            new Lexem(7)
         };

         var calc = new ShuntingYardCalculator();

         var postfNotList = calc.TranslateToPostfixNotation(lexemList).ToArray();

         Assert.IsTrue(!postfNotList[0].IsOperation);
         Assert.IsTrue(!postfNotList[1].IsOperation);
         Assert.IsTrue(postfNotList[2].IsOperation);
      }

      [TestMethod]
      public void TestMethod2()
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

         var postfNotList = calc.TranslateToPostfixNotation(lexemList).ToArray();

         // 7 2 3 * -
         Assert.IsTrue(!postfNotList[0].IsOperation);
         Assert.IsTrue(!postfNotList[1].IsOperation);
         Assert.IsTrue(!postfNotList[2].IsOperation);
         Assert.IsTrue(postfNotList[3].IsOperation);
         Assert.IsTrue(postfNotList[4].IsOperation);
         Assert.AreEqual(2, postfNotList[1].Value);
         Assert.AreEqual(14, postfNotList[3].Calculate(postfNotList[0], postfNotList[1]).Value);
      }
   }
}
