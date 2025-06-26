using Aymadoka.EfCoreMermaid.Extensions;

namespace Aymadoka.EfCoreMermaid.Entities
{
    public class RelationshipMetadata
    {
        /// <summary>源实体名称</summary>
        public string SourceEntity { get; private set; }

        /// <summary>目标实体名称</summary>
        public string TargetEntity { get; private set; }

        /// <summary>实体之间的关系类型</summary>
        public RelationshipType RelationshipType { get; private set; }

        /// <summary>导航属性名称</summary>
        public string NavigationProperty { get; private set; }

        public RelationshipMetadata(
            string sourceEntity,
            string targetEntity,
            RelationshipType relationshipType,
            string navigationProperty)
        {
            SourceEntity = sourceEntity;
            TargetEntity = targetEntity;
            RelationshipType = relationshipType;
            NavigationProperty = navigationProperty;
        }
    }
}
