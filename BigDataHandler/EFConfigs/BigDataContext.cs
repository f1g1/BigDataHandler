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
        public DbSet<DataStampsStatisticalFeatures> DataStampsStatisticalFeatures { get; set; }

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

            modelBuilder.Entity<StatisticalFeatures>().HasKey(x => x.Id);
            modelBuilder.Entity<StatisticalFeatures>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<DataStampsStatisticalFeatures>().HasKey(x => x.Id);
            modelBuilder.Entity<DataStampsStatisticalFeatures>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<DataStampsStatisticalFeatures>()
                .Property(x => x.StartTimestamp)
                .IsRequired();
            modelBuilder.Entity<DataStampsStatisticalFeatures>()
                .Property(x => x.StopTimestamp)
                .IsRequired();
            modelBuilder.Entity<DataStampsStatisticalFeatures>()
                .Property(x => x.StartLocation)
                .IsRequired();
            modelBuilder.Entity<DataStampsStatisticalFeatures>()
                .Property(x => x.StopLocation)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
