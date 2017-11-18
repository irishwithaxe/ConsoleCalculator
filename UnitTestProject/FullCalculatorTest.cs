using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Infrastructure;
using System.Linq;

namespace UnitTestProject
{
   [TestClass]
   public class FullCalculatorTest
   {
      [TestMethod]
      public void TestMethod1()
      {
         var expression = " 0.2 + 10 / 2 * 3 / 1 / 1 - 3 * 5";

         var la = new LexemParser();
         var lexems = la.Parse(expression).ToArray();

         var calc = new ShuntingYardCalculator();
         var result = calc.Calculate(lexems);

         Assert.IsTrue(Math.Abs(0.2 - result) < 0.0000001);
      }

      [TestMethod]
      public void TestMethod2()
      {
         var expression = " 7 - 2 * 3 + 1 + 1 * 1 + 2 / 1";

         var la = new LexemParser();
         var lexems = la.Parse(expression).ToArray();

         var calc = new ShuntingYardCalculator();
         var result = calc.Calculate(lexems);

         Assert.AreEqual(5, result);
      }

      [TestMethod]
      public void TestMethod3()
      {
         var expression = " 7 - 2 * 3";

         var la = new LexemParser();
         var lexems = la.Parse(expression).ToArray();

         var calc = new ShuntingYardCalculator();
         var result = calc.Calculate(lexems);

         Assert.AreEqual(1, result);
      }
   }
}
