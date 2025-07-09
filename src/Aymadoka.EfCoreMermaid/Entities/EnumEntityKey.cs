using System;
using System.ComponentModel;

namespace Aymadoka.EfCoreMermaid.Entities
{
    /// <summary>
    /// 表示实体键类型的枚举，支持位运算组合
    /// </summary>
    [Flags]
    internal enum EnumEntityKey
    {
        /// <summary>无键类型</summary>
        None = 0, // 0

        /// <summary>主键</summary>
        [Description("PK")]
        PrimaryKey = 1 << 0, // 1

        /// <summary>外键</summary>
        [Description("FK")]
        ForeignKey = 1 << 1, // 2

        /// <summary>唯一键</summary>
        [Description("UK")]
        UniqueKey = 1 << 2, // 4
    }
}
