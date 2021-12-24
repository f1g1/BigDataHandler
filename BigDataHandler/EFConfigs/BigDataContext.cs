using BigDataHandler.Models;
using Microsoft.EntityFrameworkCore;

namespace BigDataHandler.EFConfigs
{
    public class BigDataContext : DbContext
    {
        public BigDataContext(DbContextOptions<BigDataContext> options) : base(options)
        {
        }

        public DbSet<DataStamp> DataStamps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataStamp>().HasKey(x => x.Id);
            modelBuilder.Entity<DataStamp>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<DataStamp>()
               .Property(b => b.Timestamp)
               .IsRequired();

            modelBuilder.Entity<DataStamp>()
               .Property(b => b.Type)
               .IsRequired();

            modelBuilder.Entity<DataStamp>()
             .HasIndex(p => new { p.Timestamp, p.Type }).IsUnique();


            base.OnModelCreating(modelBuilder);
        }
    }
}
