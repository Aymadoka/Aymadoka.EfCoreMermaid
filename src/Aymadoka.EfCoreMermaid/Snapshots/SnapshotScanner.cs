using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Aymadoka.EfCoreMermaid.Snapshots
{
    /// <summary>
    /// 提供用于扫描并获取所有继承自 <see cref="ModelSnapshot"/> 的类型的工具方法
    /// </summary>
    public static class SnapshotScanner
    {
        /// <summary>
        /// 扫描当前 AppDomain 中所有已加载的程序集，查找所有继承自 <see cref="ModelSnapshot"/> 的非抽象类类型
        /// </summary>
        /// <returns>所有继承自 <see cref="ModelSnapshot"/> 的类型列表</returns>
        public static List<Type> GetAllModelSnapshotDerivedTypes()
        {
            // 获取所有已加载的程序集
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var allTypes = assemblies
                 .SelectMany(a =>
                 {
                     try
                     {
                         // 获取程序集中的所有类型
                         return a.GetTypes();
                     }
                     catch (ReflectionTypeLoadException ex)
                     {
                         // 某些类型加载失败时，返回已成功加载的类型
                         return ex.Types.OfType<Type>();
                     }
                 });

            var modelSnapshotType = typeof(ModelSnapshot);

            // 查找所有继承自 ModelSnapshot 的非抽象类类型
            var derivedTypes = from t in allTypes
                               where t != null && t.IsClass && !t.IsAbstract
                                    && modelSnapshotType.IsAssignableFrom(t)
                               select t;

            var result = derivedTypes.ToList();
            return result;
        }
    }
}
