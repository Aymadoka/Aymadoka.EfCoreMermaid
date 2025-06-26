using System.Collections.Generic;

namespace Aymadoka.EfCoreMermaid.Entities
{
    public class EntityMetadata
    {
        /// <summary>名称</summary>
        public string Name { get; private set; }

        /// <summary>属性集合</summary>
        public List<PropertyMetadata> Properties { get; private set; }

        public EntityMetadata(string name)
        {
            Name = name;
            Properties = new List<PropertyMetadata>();
        }

        public EntityMetadata(string name, List<PropertyMetadata> properties)
        {
            Name = name;
            Properties = properties;
        }

        public void AddProperty(PropertyMetadata property)
        {
            Properties.Add(property);
        }
    }
}
