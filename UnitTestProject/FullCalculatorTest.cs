using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Infrastructure;
using System.Linq;

namespace UnitTestProject
{
   [TestClass]
   public class FullCalculatorTest
   {
      private double Calc(string exprsn)
      {
         var la = new LexemParser();
         var lexems = la.Parse(exprsn).ToArray();

         var calc = new ShuntingYardCalculator();
         return calc.Calculate(lexems);
      }

      private bool Equal(double x1, double x2)
      {
         return Math.Abs(x1 - x2) < 0.0000001;
      }

      [TestMethod]
      public void TestMethod1()
      {
         var expected = 2 + 10 / 2 * 3 / 1 / 1 - 3 * 5;
         var exprsn = " 2 + 10 / 2 * 3 / 1 / 1 - 3 * 5";
         var result = Calc(exprsn);
         Assert.IsTrue(Equal(expected, result));
      }

      [TestMethod]
      public void TestMethod2()
      {
         var expected = 7 - 2 * 3 + 1 + 1 * 1 + 2 / 1;
         var exprsn = " 7 - 2 * 3 + 1 + 1 * 1 + 2 / 1";
         var result = Calc(exprsn);
         Assert.IsTrue(Equal(expected, result));
      }

      [TestMethod]
      public void TestMethod3()
      {
         var expected = 7 - 2 * 3;
         var exprsn = " 7 - 2 * 3";
         var result = Calc(exprsn);
         Assert.IsTrue(Equal(expected, result));
      }

      [TestMethod]
      public void TestMethod4()
      {
         var expected = 2.322448979591837; // вот какая погрешность ...
         var exprsn = " 0.2 + 10 / 2 * 1 + 3 * 2 * 8 / 7 / 7 / 8 - 1 - 1 - 1 / 2 - 2 / 2 + 1 / 2";
         var result = Calc(exprsn);
         Assert.IsTrue(Equal(expected, result));
      }

      [TestMethod]
      public void TestMethod5()
      {
         var exprsn = " 0.2 + 10 / 2 *";
         Assert.ThrowsException<ArgumentException>(() => Calc(exprsn));
      }
   }
}
