using System;

namespace Aymadoka.EfCoreMermaid.ConsoleInteractive
{
    /// <summary>
    /// 控制台渲染器，用于输出帮助信息、成功/错误消息及图表预览
    /// </summary>
    public static class ConsoleRenderer
    {
        /// <summary>
        /// 渲染帮助信息到控制台
        /// </summary>
        public static void RenderHelp()
        {
            Console.WriteLine("==== EF Core 模型快照转 Mermaid 图表工具 ====");
            Console.WriteLine("此工具会扫描项目中的 EF Core 模型快照并生成 Mermaid 图表");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine();
        }

        /// <summary>
        /// 渲染成功消息到控制台（绿色）
        /// </summary>
        /// <param name="message">要显示的消息</param>
        public static void RenderSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[成功] {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// 渲染错误消息到控制台（红色）
        /// </summary>
        /// <param name="message">要显示的消息</param>
        public static void RenderError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[错误] {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// 渲染 Mermaid 图表预览到控制台
        /// </summary>
        /// <param name="diagram">Mermaid 图表内容</param>
        public static void RenderDiagramPreview(string diagram)
        {
            Console.WriteLine("==== 图表预览 ====");
            Console.WriteLine(diagram);
            Console.WriteLine("-----------------");
        }
    }
}
