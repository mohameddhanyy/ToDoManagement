using Microsoft.EntityFrameworkCore;
using TODO.Data.Models;

namespace TODO.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<ToDo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToDo>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<ToDo>()
                .Property(t => t.Status)
                .HasConversion<string>();

            modelBuilder.Entity<ToDo>()
                .Property(t => t.Priority)
                .HasConversion<string>();
        }
    }
}
