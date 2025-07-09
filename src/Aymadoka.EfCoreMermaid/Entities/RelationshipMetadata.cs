namespace Aymadoka.EfCoreMermaid.Entities
{
    /// <summary>
    /// 表示两个实体之间的关系元数据
    /// </summary>
    internal class RelationshipMetadata
    {
        /// <summary>
        /// 源实体名称
        /// </summary>
        internal string SourceEntity { get; private set; }

        /// <summary>
        /// 目标实体名称
        /// </summary>
        internal string TargetEntity { get; private set; }

        /// <summary>
        /// 实体之间的关系类型
        /// </summary>
        internal EnumRelationshipType RelationshipType { get; private set; }

        /// <summary>
        /// 导航属性名称
        /// </summary>
        internal string NavigationProperty { get; private set; }

        /// <summary>
        /// 初始化 <see cref="RelationshipMetadata"/> 类的新实例
        /// </summary>
        /// <param name="sourceEntity">源实体名称</param>
        /// <param name="targetEntity">目标实体名称</param>
        /// <param name="relationshipType">实体之间的关系类型</param>
        /// <param name="navigationProperty">导航属性名称</param>
        internal RelationshipMetadata(
            string sourceEntity,
            string targetEntity,
            EnumRelationshipType relationshipType,
            string navigationProperty)
        {
            SourceEntity = sourceEntity;
            TargetEntity = targetEntity;
            RelationshipType = relationshipType;
            NavigationProperty = navigationProperty;
        }
    }
}
