using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Infrastructure
{
   interface ILexemAnalyzer
   {
      IEnumerable<Lexem> Parse(string expression);
   }
}
