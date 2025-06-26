using Aymadoka.EfCoreMermaid.Console.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Aymadoka.EfCoreMermaid.Console
{
    internal class Program
    {
        public static string GetDatabaseType<TEntity>(DbContext context, string propertyName)
            where TEntity : class
        {
            // 获取实体类型的属性
            PropertyInfo property = typeof(TEntity).GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(TEntity).Name}'");
            }

            // 从模型中获取实体类型的元数据
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            if (entityType == null)
            {
                throw new ArgumentException($"Entity type '{typeof(TEntity).Name}' not found in the model.");
            }

            // 获取属性的元数据
            var propertyMetadata = entityType.FindProperty(property);
            if (propertyMetadata == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found in the model for entity type '{typeof(TEntity).Name}'.");
            }

            // 返回数据库类型
            return propertyMetadata.GetColumnType();
        }

        static void Main(string[] args)
        {
            var a = new EfCoreMermaidGenerator();
            System.Console.WriteLine(a.Generate());
        }
    }
}
