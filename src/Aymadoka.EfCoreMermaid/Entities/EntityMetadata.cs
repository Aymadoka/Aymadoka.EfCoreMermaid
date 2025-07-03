using System.Collections.Generic;

namespace Aymadoka.EfCoreMermaid.Entities
{
    /// <summary>
    /// 实体元数据，包含实体名称及其属性集合
    /// </summary>
    public class EntityMetadata
    {
        /// <summary>
        /// 实体名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 实体的属性元数据集合
        /// </summary>
        public List<PropertyMetadata> Properties { get; private set; }

        /// <summary>
        /// 使用指定名称初始化 <see cref="EntityMetadata"/> 类的新实例
        /// </summary>
        /// <param name="name">实体名称</param>
        public EntityMetadata(string name)
        {
            Name = name;
            Properties = new List<PropertyMetadata>();
        }

        /// <summary>
        /// 使用指定名称和属性集合初始化 <see cref="EntityMetadata"/> 类的新实例
        /// </summary>
        /// <param name="name">实体名称</param>
        /// <param name="properties">属性元数据集合</param>
        public EntityMetadata(string name, List<PropertyMetadata> properties)
        {
            Name = name;
            Properties = properties;
        }

        /// <summary>
        /// 向属性集合中添加一个属性元数据
        /// </summary>
        /// <param name="property">要添加的属性元数据</param>
        public void AddProperty(PropertyMetadata property)
        {
            Properties.Add(property);
        }
    }
}
