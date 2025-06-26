using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aymadoka.EfCoreMermaid.Console.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }

        [MaxLength(16)]
        public string Url { get; set; }

        public decimal Amount { get; set; }

        public List<Post> Posts { get; } = new();
    }
}
