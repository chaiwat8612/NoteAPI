using Microsoft.EntityFrameworkCore;
using NoteAPI.Models.Blog;

namespace NoteAPI.Contexts.Blog
{
    public class BlogContext : DbContext, IBlogContext
    {
        public BlogContext(DbContextOptions<BlogContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<BlogModel> blogModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogModel>()
                .HasKey(m => m.blogId);
        }

        public int BlogSaveChange()
        {
            return this.SaveChanges();
        }
    }
}