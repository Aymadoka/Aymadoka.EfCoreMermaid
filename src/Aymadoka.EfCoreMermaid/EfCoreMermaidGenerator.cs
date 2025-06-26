using Aymadoka.EfCoreMermaid.Entities;
using Aymadoka.EfCoreMermaid.Extensions;
using Aymadoka.EfCoreMermaid.Snapshots;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Aymadoka.EfCoreMermaid
{
    public class EfCoreMermaidGenerator : IEfCoreMermaidGenerator
    {
        /// <summary>
        /// 获取所有继承自 ModelSnapshot 的类型
        /// </summary>
        /// <returns>所有继承自 ModelSnapshot 的类型列表</returns>
        public static List<Type> GetAllModelSnapshotDerivedTypes()
        {
            // 获取所有已加载的程序集
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var modelSnapshotType = typeof(ModelSnapshot);
            var derivedTypes = assemblies
                .SelectMany(a =>
                {
                    try
                    {
                        return a.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        return ex.Types.Where(t => t != null)!;
                    }
                })
                .Where(t => t != null && t.IsClass && !t.IsAbstract && modelSnapshotType.IsAssignableFrom(t))
                .ToList();

            return derivedTypes;
        }

        /// <summary>
        /// 生成 Mermaid ER 图的字符串表示
        /// </summary>
        /// <param name="modelMetadata">模型元数据，包含实体及其关系信息</param>
        /// <returns>Mermaid ER 图的字符串</returns>
        private static string GenerateMermaidErDiagram(ModelMetadata modelMetadata)
        {
            var sb = new StringBuilder();
            sb.AppendLine("erDiagram");
            sb.AppendLine(string.Empty);

            modelMetadata.Entities.ForEach(entity =>
            {
                sb.AppendLine($"    {entity.Name} {{");
                foreach (var prop in entity.Properties)
                {
                    string key = string.Empty;
                    if ((prop.Key & EnumEntityKey.None) == EnumEntityKey.None)
                    {
                        var desc = prop.Key.GetDescription(", ");
                        key += (" " + desc);
                    }

                    string comment = string.Empty;
                    if (!string.IsNullOrWhiteSpace(prop.Comment))
                    {
                        comment += (" \"" + prop.Comment.Trim() + "\"");
                    }

                    sb.AppendLine($"        {prop.Type} {prop.Name}{key}{comment}");
                }

                sb.AppendLine("    }");
            });

            modelMetadata.Relationships.ForEach(relationship =>
            {
                // Mermaid ER 图关系语法: <Entity1> <relation> <Entity2> : <label>
                // 关系类型映射
                var relationSymbol = relationship.RelationshipType switch
                {
                    RelationshipType.OneToOne => "||--||",
                    RelationshipType.OneToMany => "||--o{",
                    RelationshipType.ManyToOne => "}o--||",
                    RelationshipType.ManyToMany => "}o--o{",
                    _ => "--"
                };

                // 导航属性作为 label
                string label = string.IsNullOrWhiteSpace(relationship.NavigationProperty)
                    ? ""
                    : $" : {relationship.NavigationProperty}";

                sb.AppendLine($"    {relationship.SourceEntity} {relationSymbol} {relationship.TargetEntity}{label}");
            });

            return sb.ToString();
        }

        public string Generate()
        {
            var modelSnapshotTypes = GetAllModelSnapshotDerivedTypes();

            var modelMetadata = SnapshotParser.ParseSnapshotType(modelSnapshotTypes[0]);

            var result = GenerateMermaidErDiagram(modelMetadata);

            return result;
        }
    }
}
