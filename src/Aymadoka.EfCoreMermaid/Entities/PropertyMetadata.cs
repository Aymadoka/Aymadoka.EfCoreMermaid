using Aymadoka.EfCoreMermaid.Extensions;

namespace Aymadoka.EfCoreMermaid.Entities
{
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
