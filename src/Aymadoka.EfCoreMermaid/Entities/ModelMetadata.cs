using System.Collections.Generic;

namespace Aymadoka.EfCoreMermaid.Entities
{
    /// <summary>
    /// 表示模型的元数据，包含所有实体及其关系的集合
    /// </summary>
    internal class ModelMetadata
    {
        /// <summary>
        /// 获取实体元数据的集合
        /// </summary>
        internal List<EntityMetadata> Entities { get; private set; }

        /// <summary>
        /// 获取实体关系元数据的集合
        /// </summary>
        internal List<RelationshipMetadata> Relationships { get; private set; }

        /// <summary>
        /// 初始化 <see cref="ModelMetadata"/> 类的新实例
        /// </summary>
        internal ModelMetadata()
        {
            Entities = new List<EntityMetadata>();
            Relationships = new List<RelationshipMetadata>();
        }

        /// <summary>
        /// 向实体集合中添加一个实体元数据
        /// </summary>
        /// <param name="entity">要添加的实体元数据</param>
        internal void AddEntity(EntityMetadata entity)
        {
            Entities.Add(entity);
        }

        /// <summary>
        /// 向关系集合中添加一个关系元数据
        /// </summary>
        /// <param name="relationship">要添加的关系元数据</param>
        internal void AddRelationship(RelationshipMetadata relationship)
        {
            Relationships.Add(relationship);
        }
    }
}
