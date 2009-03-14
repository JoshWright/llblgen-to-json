using System.Text.RegularExpressions;

namespace LLBLGenExtensions.ExtensionMethods
{
    public static class StringExtensions
    {

        public static string UnPascalCase(this string text)
        {
            return new Regex(@"[A-Z]").Replace(text, @" $0").ToLower().Trim();
        }

        public static string CapitalizeFirst(this string text)
        {
            if (text.Length <= 1)
                return text.ToUpper();
            return text[0].ToString().ToUpper() + text.Substring(1);
        }
    }
}
