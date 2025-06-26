using System.ComponentModel;

namespace Aymadoka.EfCoreMermaid.Console.Entities
{
    public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        [Description]
        public string Content { get; set; }

        public int BlogId { get; set; }

        public Blog Blog { get; set; }
    }
}
