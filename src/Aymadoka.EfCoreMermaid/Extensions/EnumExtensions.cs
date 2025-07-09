using System;
using System.ComponentModel;
using System.Linq;

namespace Aymadoka.EfCoreMermaid.Extensions
{
    internal static class EnumExtensions
    {
        /// <summary>
        /// 判断枚举类型是否带有 <see cref="FlagsAttribute"/> 特性
        /// </summary>
        /// <param name="this">要检查的枚举实例</param>
        /// <returns>如果枚举类型带有 <see cref="FlagsAttribute"/>，则返回 true；否则返回 false</returns>
        internal static bool IsFlags<TEnum>(this TEnum @this) where TEnum : Enum
        {
            var type = @this.GetType();
            if (!type.IsEnum)
            {
                return false;
            }

            var isFlags = type.IsDefined(typeof(FlagsAttribute), false);
            return isFlags;
        }


        /// <summary>
        /// 获取枚举值的描述信息
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="this">要获取描述的枚举实例。</param>
        /// <param name="separator">当枚举为 Flags 类型且有多个值时，描述之间的分隔符，默认为 ", "</param>
        /// <returns>枚举值的描述信息；如果为 Flags 且未选中任何标志，则返回空字符串</returns>
        /// <exception cref="ArgumentNullException">当 <paramref name="this"/> 为 null 时抛出</exception>
        internal static string GetDescription<TEnum>(this TEnum @this, string separator = ", ") where TEnum : Enum
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            var type = @this.GetType();
            var isFlags = @this.IsFlags();
            if (isFlags)
            {
                var values = Enum.GetValues(type).Cast<Enum>();
                var selectedFlags = values.Where(v => @this.HasFlag(v) && Convert.ToInt64(v) != 0).ToArray();
                if (selectedFlags.Length == 0)
                {
                    return string.Empty;
                }

                var descriptions = selectedFlags.Select(flag =>
                {
                    var field = type.GetField(flag.ToString());
                    var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                         .OfType<DescriptionAttribute>()
                                         .FirstOrDefault();
                    return attribute?.Description ?? flag.ToString();
                });

                return string.Join(separator, descriptions);
            }
            else
            {
                var field = type.GetField(@this.ToString());
                var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                     .OfType<DescriptionAttribute>()
                                     .FirstOrDefault();

                return attribute?.Description ?? @this.ToString();
            }
        }
    }
}
