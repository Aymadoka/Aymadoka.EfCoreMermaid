namespace Aymadoka.EfCoreMermaid.Entities
{
    /// <summary>
    /// 表示实体之间的关系类型
    /// </summary>
    internal enum EnumRelationshipType
    {
        /// <summary>
        /// 一对一关系
        /// </summary>
        OneToOne,

        /// <summary>
        /// 一对多关系
        /// </summary>
        OneToMany,

        /// <summary>
        /// 多对一关系
        /// </summary>
        ManyToOne,

        /// <summary>
        /// 多对多关系
        /// </summary>
        ManyToMany,

        /// <summary>
        /// 拥有类型（Owned Entity）
        /// </summary>
        Owned
    }
}
