using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.Infrastructure;
using System.Linq;

namespace UnitTestProject
{
   [TestClass]
   public class LexemParserTest
   {
      [TestMethod]
      public void ParseTest1()
      {
         var expression = string.Empty;
         var la = new LexemParser();
         var lexems = la.Parse(expression);

         Assert.AreEqual(0, lexems.Count());
      }

      [TestMethod]
      public void ParseTest2()
      {
         var expression = "5+3";
         var la = new LexemParser();
         var lexems = la.Parse(expression).ToArray();

         var strexpr = string.Join(" ", lexems.Select(x => x.ToString()));
         Assert.AreEqual("5 + 3", strexpr);
      }

      [TestMethod]
      public void ParseTest3()
      {
         var expression = " 52 +   87  ";
         var la = new LexemParser();
         var lexems = la.Parse(expression).ToArray();

         var strexpr = string.Join(" ", lexems.Select(x => x.ToString()));
         Assert.AreEqual("52 + 87", strexpr);
      }

      [TestMethod]
      public void ParseTest4()
      {
         var expression = " 52 +   87  / 22 * 41234*** +   2++ ///";
         var la = new LexemParser();
         var lexems = la.Parse(expression).ToArray();

         var strexpr = string.Join(" ", lexems.Select(x => x.ToString()));
         Assert.AreEqual("52 + 87 / 22 * 41234 * * * + 2 + + / / /", strexpr);
      }

      [TestMethod]
      public void GetSymbolTest1()
      {
         var expression = "5 +3";
         var la = new LexemParser();
         Assert.AreEqual(LexemParser.SymbolType.number, la.GetSymbolType(expression, 0));
      }

      [TestMethod]
      public void GetSymbolTest2()
      {
         var expression = "5+3";
         var la = new LexemParser();
         Assert.AreEqual(LexemParser.SymbolType.operationAddition, la.GetSymbolType(expression, 1));
      }

      [TestMethod]
      public void GetSymbolTest3()
      {
         var expression = "   5+ 3";
         var la = new LexemParser();
         Assert.AreEqual(LexemParser.SymbolType.whitespace, la.GetSymbolType(expression, 1));
      }

      [TestMethod]
      public void GetSymbolTest4()
      {
         var expression = "5 +0.3";
         var la = new LexemParser();
         Assert.AreEqual(LexemParser.SymbolType.number, la.GetSymbolType(expression, 4));
      }

      [TestMethod]
      public void GetSymbolTest5()
      {
         var expression = "5 +0.3";
         var la = new LexemParser();
         Assert.ThrowsException<ArgumentException>(() => la.GetSymbolType(expression, 17));
      }
   }
}
