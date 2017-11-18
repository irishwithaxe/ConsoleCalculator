using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Infrastructure
{
   public static class Extensions
   {
      public static string F(this string str, object arg0)
      {
         return string.Format(str, arg0);
      }

      public static string F(this string str, object arg0, object arg1)
      {
         return string.Format(str, arg0, arg1);
      }
   }
}
