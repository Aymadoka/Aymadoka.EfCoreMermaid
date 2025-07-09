using System.Reflection;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Aymadoka.EfCoreMermaid.Entities;

namespace Aymadoka.EfCoreMermaid.Extensions
{
    internal static class PropertyExtensions
    {
        /// <summary>
        /// 获取属性的注释（Comment）或描述（DescriptionAttribute）。
        /// 优先返回数据库注释，如果没有则返回 DescriptionAttribute 的描述。
        /// </summary>
        /// <param name="property">EF Core 的属性元数据。</param>
        /// <returns>注释或描述，如果都没有则返回属性名。</returns>
        internal static string GetCommentOrDescription(this IProperty property)
        {
            // 优先获取数据库注释
            var comment = property.GetComment();
            if (!string.IsNullOrWhiteSpace(comment))
            {
                return comment;
            }

            // 尝试获取 DescriptionAttribute
            var clrProperty = property.PropertyInfo;
            if (clrProperty != null)
            {
                var descriptionAttr = clrProperty.GetCustomAttribute<DescriptionAttribute>();
                if (descriptionAttr != null && !string.IsNullOrWhiteSpace(descriptionAttr.Description))
                {
                    return descriptionAttr.Description;
                }
            }

            return string.Empty;
        }

        internal static EnumEntityKey GetKeyConstraints(this IProperty property)
        {
            var keyType = EnumEntityKey.None;
            if (property.IsPrimaryKey())
            {
                keyType |= EnumEntityKey.PrimaryKey;
            }
            if (property.IsForeignKey())
            {
                keyType |= EnumEntityKey.ForeignKey;
            }

            return keyType;
        }

        internal static bool IsRequired(this IProperty property)
        {
            return !property.IsNullable;
        }
    }
}
