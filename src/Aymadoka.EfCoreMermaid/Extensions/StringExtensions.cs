using System;

namespace Aymadoka.EfCoreMermaid.Extensions
{
    public static class StringExtensions
    {
        public static string LastPart(this string @this, string separator)
        {
            if (string.IsNullOrEmpty(@this))
            {
                return @this;
            }

            if (string.IsNullOrEmpty(separator))
            {
                throw new ArgumentException("Separator cannot be empty.", nameof(separator));
            }

            var lastIndex = @this.LastIndexOf(separator);
            return lastIndex >= 0 ? @this.Substring(lastIndex + separator.Length) : @this;
        }

        /// <summary>
        /// 将包含 "decimal" 的字符串中的英文逗号替换为中文逗号，以保证在 Mermaid 中的安全性。
        /// </summary>
        /// <param name="this">要处理的字符串</param>
        /// <returns>替换后的字符串，如果不包含 "decimal" 则返回原字符串</returns>
        public static string ToMermaidSafeDecimalType(this string @this)
        {
            if (@this != null && @this.Contains("decimal"))
            {
                return @this.Replace(",", "，");
            }

            return @this;
        }
    }
}
