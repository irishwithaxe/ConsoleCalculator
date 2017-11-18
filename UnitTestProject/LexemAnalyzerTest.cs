using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Infrastructure;
using System.Linq;

namespace UnitTestProject
{
   [TestClass]
   public class LexemAnalyzerTest
   {
      [TestMethod]
      public void ParseTest1()
      {
         var expression = string.Empty;
         var la = new LexemAnalyzer();
         var lexems = la.Parse(expression);

         Assert.AreEqual(0, lexems.Count());
      }

      [TestMethod]
      public void ParseTest2()
      {
         var expression = "5 +3";
         var la = new LexemAnalyzer();
         var lexems = la.Parse(expression).ToArray();

         Assert.AreEqual(3, lexems.Length);
         Assert.AreEqual(3.0, lexems[2].Value);
         Assert.IsTrue(!lexems[1].IsNumber);

         var val = lexems[1].Operation(lexems[0], lexems[2]);
         Assert.AreEqual(8.0, val.Value);
      }

      [TestMethod]
      public void ParseTest3()
      {
         var expression = " 5.2 +   87  ";
         var la = new LexemAnalyzer();
         var lexems = la.Parse(expression).ToArray();

         Assert.AreEqual(3, lexems.Length);
         Assert.AreEqual(87.0, lexems[2].Value);
         Assert.IsTrue(!lexems[1].IsNumber);

         var val = lexems[1].Operation(lexems[0], lexems[2]);
         Assert.AreEqual(92.2, val.Value);
      }

      [TestMethod]
      public void ParseTest4()
      {
         var expression = " 5.2 +   87  / 2.2 * 0.41234 +   2++ ///";
         var la = new LexemAnalyzer();
         var lexems = la.Parse(expression).ToArray();

         Assert.AreEqual(13, lexems.Length);
         Assert.AreEqual(87.0, lexems[2].Value);
         Assert.IsTrue(!lexems[12].IsNumber);
      }

      [TestMethod]
      public void GetSymbolTest1()
      {
         var expression = "5 +3";
         var la = new LexemAnalyzer();
         Assert.AreEqual(LexemAnalyzer.SymbolType.number, la.GetSymbolType(expression, 0));
      }

      [TestMethod]
      public void GetSymbolTest2()
      {
         var expression = "5 +3";
         var la = new LexemAnalyzer();
         Assert.AreEqual(LexemAnalyzer.SymbolType.operationAddition, la.GetSymbolType(expression, 2));
      }

      [TestMethod]
      public void GetSymbolTest3()
      {
         var expression = "5 +3";
         var la = new LexemAnalyzer();
         Assert.AreEqual(LexemAnalyzer.SymbolType.whitespace, la.GetSymbolType(expression, 1));
      }

      [TestMethod]
      public void GetSymbolTest4()
      {
         var expression = "5 +0.3";
         var la = new LexemAnalyzer();
         Assert.AreEqual(LexemAnalyzer.SymbolType.number, la.GetSymbolType(expression, 4));
      }

      [TestMethod]
      public void GetSymbolTest5()
      {
         var expression = "5 +0.3";
         var la = new LexemAnalyzer();
         Assert.ThrowsException<ArgumentException>(() => la.GetSymbolType(expression, 17));
      }
   }
}
