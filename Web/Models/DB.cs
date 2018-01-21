using Microsoft.EntityFrameworkCore;

namespace Web.Models
{
    public sealed class DB : DbContext
    {
        public DbSet<Page> Pages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Page>().ToTable("Pages");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(Settings.ConnectionString);
        }
    }
}