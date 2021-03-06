// <auto-generated />
using System;
using BigDataHandler.EFConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BigDataHandler.Migrations
{
    [DbContext(typeof(BigDataContext))]
    partial class BigDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BigDataHandler.Models.CartesianStatisticalFeatures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("xAxisFeaturesId")
                        .HasColumnType("int");

                    b.Property<int?>("yAxisFeaturesId")
                        .HasColumnType("int");

                    b.Property<int?>("zAxisFeaturesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("xAxisFeaturesId");

                    b.HasIndex("yAxisFeaturesId");

                    b.HasIndex("zAxisFeaturesId");

                    b.ToTable("CartesianStatisticalFeatures");
                });

            modelBuilder.Entity("BigDataHandler.Models.DataStamp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ActivityStartTimestamp")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("bit");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Timestamp")
                        .HasColumnType("bigint");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Values")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Timestamp", "Type")
                        .IsUnique();

                    b.ToTable("DataStamps");
                });

            modelBuilder.Entity("BigDataHandler.Models.DataStampsStatisticalFeatures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ActivityStartTimestamp")
                        .HasColumnType("bigint");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsProcessed")
                        .HasColumnType("bit");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("StartTimestamp")
                        .HasColumnType("bigint");

                    b.Property<string>("StopLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("StopTimestamp")
                        .HasColumnType("bigint");

                    b.Property<int?>("phoneAccelerometerStatisticsId")
                        .HasColumnType("int");

                    b.Property<int?>("phoneGyroscopeStatisticsId")
                        .HasColumnType("int");

                    b.Property<int?>("sensorAccelerometerStatisticsId")
                        .HasColumnType("int");

                    b.Property<int?>("sensorGyroscopeStatisticsId")
                        .HasColumnType("int");

                    b.Property<int>("stepsCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("phoneAccelerometerStatisticsId");

                    b.HasIndex("phoneGyroscopeStatisticsId");

                    b.HasIndex("sensorAccelerometerStatisticsId");

                    b.HasIndex("sensorGyroscopeStatisticsId");

                    b.ToTable("DataStampsStatisticalFeatures");

                    b.HasDiscriminator<string>("Discriminator").HasValue("DataStampsStatisticalFeatures");
                });

            modelBuilder.Entity("BigDataHandler.Models.StatisticalFeatures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Max")
                        .HasColumnType("float");

                    b.Property<double>("Mean")
                        .HasColumnType("float");

                    b.Property<double>("Min")
                        .HasColumnType("float");

                    b.Property<double>("StandardDeviation")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("StatisticalFeatures");
                });

            modelBuilder.Entity("BigDataHandler.Models.DataStampsStatisticalFeaturesPredicted", b =>
                {
                    b.HasBaseType("BigDataHandler.Models.DataStampsStatisticalFeatures");

                    b.HasDiscriminator().HasValue("DataStampsStatisticalFeaturesPredicted");
                });

            modelBuilder.Entity("BigDataHandler.Models.CartesianStatisticalFeatures", b =>
                {
                    b.HasOne("BigDataHandler.Models.StatisticalFeatures", "xAxisFeatures")
                        .WithMany()
                        .HasForeignKey("xAxisFeaturesId");

                    b.HasOne("BigDataHandler.Models.StatisticalFeatures", "yAxisFeatures")
                        .WithMany()
                        .HasForeignKey("yAxisFeaturesId");

                    b.HasOne("BigDataHandler.Models.StatisticalFeatures", "zAxisFeatures")
                        .WithMany()
                        .HasForeignKey("zAxisFeaturesId");

                    b.Navigation("xAxisFeatures");

                    b.Navigation("yAxisFeatures");

                    b.Navigation("zAxisFeatures");
                });

            modelBuilder.Entity("BigDataHandler.Models.DataStampsStatisticalFeatures", b =>
                {
                    b.HasOne("BigDataHandler.Models.CartesianStatisticalFeatures", "phoneAccelerometerStatistics")
                        .WithMany()
                        .HasForeignKey("phoneAccelerometerStatisticsId");

                    b.HasOne("BigDataHandler.Models.CartesianStatisticalFeatures", "phoneGyroscopeStatistics")
                        .WithMany()
                        .HasForeignKey("phoneGyroscopeStatisticsId");

                    b.HasOne("BigDataHandler.Models.CartesianStatisticalFeatures", "sensorAccelerometerStatistics")
                        .WithMany()
                        .HasForeignKey("sensorAccelerometerStatisticsId");

                    b.HasOne("BigDataHandler.Models.CartesianStatisticalFeatures", "sensorGyroscopeStatistics")
                        .WithMany()
                        .HasForeignKey("sensorGyroscopeStatisticsId");

                    b.Navigation("phoneAccelerometerStatistics");

                    b.Navigation("phoneGyroscopeStatistics");

                    b.Navigation("sensorAccelerometerStatistics");

                    b.Navigation("sensorGyroscopeStatistics");
                });
#pragma warning restore 612, 618
        }
    }
}
