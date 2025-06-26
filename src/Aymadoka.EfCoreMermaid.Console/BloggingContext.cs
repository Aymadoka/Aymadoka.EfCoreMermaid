using Aymadoka.EfCoreMermaid.Console.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Aymadoka.EfCoreMermaid.Console
{
    /// <summary>
    /// 实体上下文
    /// 所有实体需定义在此
    /// </summary>
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True;ConnectRetryCount=0");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .Property(b => b.Content)
                .HasMaxLength(99)
                .HasComment("The URL of the blog");
        }
    }
}