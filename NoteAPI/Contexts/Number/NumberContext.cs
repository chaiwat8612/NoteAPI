using Microsoft.EntityFrameworkCore;
using NoteAPI.Models.Number;

namespace NoteAPI.Contexts.Number
{
    public class NumberContext : DbContext, INumberContext
    {
        public NumberContext(DbContextOptions<NumberContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<NumberModel> numberModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NumberModel>()
                .HasKey(m => m.numberId);
        }

        public int NumberSaveChange()
        {
            return this.SaveChanges();
        }
    }
}