using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace LLBLGenExtensions.ExtensionMethods
{
    public static class JSonExtensions
    {

        public static string ToJSon(this IList<string> list)
        {
            return "[" + String.Join(",", list.ToArray()) + "]";
        }

        public static string EscapeForJsonization(this string json)
        {
            return json.Replace("\"", "\\\"").Replace("$", "\\$").Replace("\r", "\\r").Replace("\n", "\\n");
        }

    }
}
