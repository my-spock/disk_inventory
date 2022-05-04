using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class Operation
    {
        public static bool IsAdd(string action) => EqualsNoCase(action, "add");
        public static bool IsDelete(string action) => EqualsNoCase(action, "delete");

        private static bool EqualsNoCase(string s, string tocompare) =>
            s?.ToLower() == tocompare?.ToLower();

        public static string IsNullVal(object s) => s == null ? null : s.ToString();
    }
}
