using Aymadoka.EfCoreMermaid.Extensions;

namespace Aymadoka.EfCoreMermaid.Entities
{
    /// <summary>
    /// 表示实体属性的元数据，包括名称、类型、是否必填、键类型及注释信息。
    /// </summary>
    public class PropertyMetadata
    {
        /// <summary>名称</summary>
        public string Name { get; set; }

        /// <summary>类型</summary>
        public string Type { get; private set; }

        /// <summary>是否必填</summary>
        public bool IsRequired { get; private set; }

        /// <summary>键</summary>
        public EnumEntityKey Key { get; private set; }

        /// <summary>评论</summary>
        public string? Comment { get; private set; }

        /// <summary>
        /// 初始化 <see cref="PropertyMetadata"/> 类的新实例
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="type">属性类型</param>
        /// <param name="isRequired">是否为必填项</param>
        /// <param name="key">属性的键类型</param>
        /// <param name="comment">属性的注释信息</param>
        public PropertyMetadata(
            string name,
            string type,
            bool isRequired,
            EnumEntityKey key,
            string? comment)
        {
            Name = name;
            Type = type.ToMermaidSafeDecimalType();
            IsRequired = isRequired;
            Key = key;
            Comment = comment;
        }
    }
}
