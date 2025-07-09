using System;
using System.Text;
using Aymadoka.EfCoreMermaid.ConsoleInteractive;
using Aymadoka.EfCoreMermaid.Entities;
using Aymadoka.EfCoreMermaid.Extensions;
using Aymadoka.EfCoreMermaid.Snapshots;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Aymadoka.EfCoreMermaid.Generators
{
    /// <summary>
    /// 提供将 EF Core 模型元数据生成 Mermaid ER 图和类图的功能
    /// </summary>
    public class EfCoreMermaidGenerator<T> where T : ModelSnapshot
    {
        /// <summary>
        /// 根据关系元数据获取 Mermaid ER 图的关系符号
        /// </summary>
        /// <param name="relationship">关系元数据</param>
        /// <returns>Mermaid ER 图关系符号</returns>
        internal static string GetRelationshipSymbol(RelationshipMetadata relationship)
        {
            var relationSymbol = relationship.RelationshipType switch
            {
                EnumRelationshipType.OneToOne => "||--||",
                EnumRelationshipType.OneToMany => "||--o{",
                EnumRelationshipType.ManyToOne => "}o--||",
                EnumRelationshipType.ManyToMany => "}o--o{",
                _ => "--"
            };

            return relationSymbol;
        }

        /// <summary>
        /// 根据模型元数据生成 Mermaid ER 图文本
        /// </summary>
        /// <param name="modelMetadata">模型元数据</param>
        /// <returns>Mermaid ER 图文本</returns>
        internal static string GenerateErDiagram(ModelMetadata modelMetadata)
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
                        key += " " + desc;
                    }

                    string comment = string.Empty;
                    if (!string.IsNullOrWhiteSpace(prop.Comment))
                    {
                        comment += " \"" + prop.Comment.Trim() + "\"";
                    }

                    sb.AppendLine($"        {prop.Type} {prop.Name}{key}{comment}");
                }

                sb.AppendLine("    }");
            });

            modelMetadata.Relationships.ForEach(relationship =>
            {
                var relationSymbol = GetRelationshipSymbol(relationship);

                // 导航属性作为 label
                string label = string.IsNullOrWhiteSpace(relationship.NavigationProperty) ? "" : $" : {relationship.NavigationProperty}";

                sb.AppendLine($"    {relationship.SourceEntity} {relationSymbol} {relationship.TargetEntity}{label}");
            });

            return sb.ToString();
        }

        /// <summary>
        /// 生成当前模型的 Mermaid ER 图文本
        /// </summary>
        /// <returns>Mermaid ER 图文本</returns>
        public string GenerateErDiagram()
        {
            ConsoleRenderer.RenderHelp();

            try
            {
                var modelMetadata = SnapshotParser.ParseSnapshot(typeof(T));

                var result = GenerateErDiagram(modelMetadata);

                ConsoleRenderer.RenderSuccess(typeof(T).Name);

                ConsoleRenderer.RenderDiagramPreview(result);

                return result;
            }
            catch (Exception ex)
            {
                var exMsg = ex.GetDeepestInnerException();

                ConsoleRenderer.RenderError(exMsg?.Message ?? ex.ToString());

                return string.Empty;
            }
        }
    }
}
