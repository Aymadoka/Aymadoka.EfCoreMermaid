using System;
using System.Diagnostics.CodeAnalysis;

namespace Aymadoka.EfCoreMermaid.Extensions
{
    internal static class ExceptionExtensions
    {
        /// <summary>获取异常链中最深层的 <see cref="Exception.InnerException"/></summary>
        /// <param name="this">要检查的异常对象，可以为 <c>null</c></param>
        /// <returns>
        /// 如果存在嵌套的 <see cref="Exception.InnerException"/>，则返回最深层的异常；
        /// 如果没有嵌套异常，则返回原始异常；如果输入为 <c>null</c>，则返回 <c>null</c>。
        /// </returns>
        internal static Exception? GetDeepestInnerException([NotNullIfNotNull(nameof(@this))] this Exception? @this)
        {
            while (@this != null && @this.InnerException != null)
            {
                @this = @this.InnerException;
            }

            return @this;
        }
    }
}
