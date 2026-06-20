using Microsoft.EntityFrameworkCore;
using System.Data;
using wayfarer.PostsService.DataAccess.DataModels;

namespace wayfarer.PostsService.DataAccess
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>(p =>
            {
                // Set custom table name and database schema
                p.ToTable("Post", schema: "post_service");
                p.HasKey(x => x.Id);
            });

            // Seed data using ModelBuilder
            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, UserId = 1, PostType = "This is the first post.", Url = "https://example.com/post1", Timestamp = DateTime.UtcNow },
                new Post { Id = 2, UserId = 1, PostType = "This is the second post.", Url = "https://example.com/post2", Timestamp = DateTime.UtcNow },
                new Post { Id = 3, UserId = 1, PostType = "This is the third post.", Url = "https://example.com/post3", Timestamp = DateTime.UtcNow },
                new Post { Id = 4, UserId = 1, PostType = "This is the fourth post.", Url = "https://example.com/post4", Timestamp = DateTime.UtcNow },
                new Post { Id = 5, UserId = 1, PostType = "This is the fifth post.", Url = "https://example.com/post5", Timestamp = DateTime.UtcNow }
            );
        }
    }
}
