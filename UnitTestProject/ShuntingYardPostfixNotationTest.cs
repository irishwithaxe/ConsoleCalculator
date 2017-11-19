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
         var strexpr = string.Join(" ", postfNotList.Select(x => x.ToString()));

         Assert.AreEqual("7 2 3 * -", strexpr);
      }

      [TestMethod]
      public void TestMethod3()
      {
         // 7 - 2 * 3 + 1 + 1 * 1 + 2 / 1;
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

         var postfNotList = calc.TranslateToPostfixNotation(lexemList).ToArray();
         var strexpr = string.Join(" ", postfNotList.Select(x => x.ToString()));

         Assert.AreEqual("7 2 3 * - 1 + 1 1 * + 2 1 / +", strexpr);
      }

      [TestMethod]
      public void TestMethod4()
      {
         var expression = "2 + 10 / 2 * 3 / 1 / 1 - 3 * 5";

         var la = new LexemParser();
         var lexems = la.Parse(expression).ToArray();

         var calc = new ShuntingYardCalculator();
         var postfNotList = calc.TranslateToPostfixNotation(lexems).ToArray();
         var strexpr = string.Join(" ", postfNotList.Select(x => x.ToString()));

         Assert.AreEqual("2 10 2 / 3 * 1 / 1 / + 3 5 * -", strexpr);
      }

      [TestMethod]
      public void TestMethod5()
      {
         var expression = "2 + 10 / 2 * 1";

         var la = new LexemParser();
         var lexems = la.Parse(expression).ToArray();

         var calc = new ShuntingYardCalculator();
         var postfNotList = calc.TranslateToPostfixNotation(lexems).ToArray();
         var strexpr = string.Join(" ", postfNotList.Select(x => x.ToString()));

         Assert.AreEqual("2 10 2 / 1 * +", strexpr);
      }
   }
}
