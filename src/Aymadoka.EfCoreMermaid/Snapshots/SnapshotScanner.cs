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
        /// 获取当前应用程序域中所有继承自 <see cref="ModelSnapshot"/> 的非抽象类类型
        /// </summary>
        /// <returns>所有继承自 <see cref="ModelSnapshot"/> 的类型列表</returns>
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
                        // 获取程序集中的所有类型
                        return a.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        // 某些类型加载失败时，返回已成功加载的类型
                        return ex.Types.Where(t => t != null)!;
                    }
                })
                // 过滤出继承自 ModelSnapshot 的非抽象类
                .Where(t => t != null && t.IsClass && !t.IsAbstract && modelSnapshotType.IsAssignableFrom(t))
                .ToList();

            return derivedTypes;
        }
    }
}
