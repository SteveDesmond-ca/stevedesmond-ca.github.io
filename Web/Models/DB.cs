using Microsoft.EntityFrameworkCore;

namespace Web.Models
{
    public sealed class DB : DbContext
    {
        public DbSet<Page> Pages { get; set; }

        public DB(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Page>().ToTable("Pages");
        }

    }
}