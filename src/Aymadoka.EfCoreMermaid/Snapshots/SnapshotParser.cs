using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Aymadoka.EfCoreMermaid.Entities;
using Aymadoka.EfCoreMermaid.Extensions;

namespace Aymadoka.EfCoreMermaid.Snapshots
{
    /// <summary>
    /// 提供用于解析 EF Core 快照并生成模型元数据的功能
    /// </summary>
    internal static class SnapshotParser
    {
        /// <summary>
        /// 解析指定类型的快照，生成模型元数据
        /// </summary>
        /// <param name="snapshotType">快照类型，通常为继承自 <see cref="ModelSnapshot"/> 的类型</param>
        /// <returns>解析得到的 <see cref="ModelMetadata"/> 实例</returns>
        internal static ModelMetadata ParseSnapshot(Type snapshotType)
        {
            var modelMetadata = new ModelMetadata();

            if (snapshotType != null && Activator.CreateInstance(snapshotType) is ModelSnapshot snapshot)
            {
                var modelBuilder = new ModelBuilder(new ConventionSet());

#pragma warning disable S3011
                var method = snapshotType.GetMethod("BuildModel", BindingFlags.Instance | BindingFlags.NonPublic);
#pragma warning restore S3011

                if (method != null)
                {
                    method.Invoke(snapshot, new object[] { modelBuilder });

                    // 生成实体定义
                    foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                    {
                        var properties = entityType.GetProperties()
                            .Select(property =>
                            {
                                var item = new PropertyMetadata(
                                    property.GetColumnName(),
                                    property.GetColumnType(),
                                    property.IsRequired(),
                                    property.GetKeyConstraints(),
                                    property.GetCommentOrDescription());

                                return item;
                            })
                            .ToList();

                        var entityMetadata = new EntityMetadata(entityType.GetTableName(), properties);
                        modelMetadata.AddEntity(entityMetadata);

                        entityType.GetForeignKeys()
                            .ForEach(relationship =>
                            {
                                var sourceEntity = relationship.DeclaringEntityType.GetTableName();
                                var targetEntity = relationship.PrincipalEntityType.GetTableName();

                                // 修正关系类型判断逻辑
                                EnumRelationshipType relationshipType;
                                if (relationship.IsUnique)
                                {
                                    relationshipType = EnumRelationshipType.OneToOne;
                                }
                                else
                                {
                                    relationshipType = EnumRelationshipType.OneToMany;
                                }
                                // 多对多关系可根据需要扩展

                                // 导航属性名称，优先 DependentToPrincipal
                                string navigationProperty = relationship.DependentToPrincipal?.Name ?? relationship.PrincipalToDependent?.Name ?? string.Empty;

                                var relationshipMetadata = new RelationshipMetadata(
                                    sourceEntity,
                                    targetEntity,
                                    relationshipType,
                                    navigationProperty);

                                modelMetadata.AddRelationship(relationshipMetadata);
                            });
                    }
                }
            }

            return modelMetadata;
        }
    }
}
