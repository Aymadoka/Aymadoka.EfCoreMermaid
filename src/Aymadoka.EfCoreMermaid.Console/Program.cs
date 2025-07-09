using Aymadoka.EfCoreMermaid.Console.Migrations;
using Aymadoka.EfCoreMermaid.Generators;

namespace Aymadoka.EfCoreMermaid.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var generator = new EfCoreMermaidGenerator<BloggingContextModelSnapshot>();
            generator.GenerateErDiagram();
        }
    }
}
