using Microsoft.EntityFrameworkCore.Migrations;

namespace BigDataHandler.Migrations
{
    public partial class AddedIsProcessedAndFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "DataStamps",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "StatisticalFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Min = table.Column<double>(type: "float", nullable: false),
                    Max = table.Column<double>(type: "float", nullable: false),
                    Mean = table.Column<double>(type: "float", nullable: false),
                    StandardDeviation = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticalFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataStampsStatisticalFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTimestamp = table.Column<long>(type: "bigint", nullable: false),
                    StopTimestamp = table.Column<long>(type: "bigint", nullable: false),
                    GyroXFeaturesId = table.Column<int>(type: "int", nullable: true),
                    GyroYFeaturesId = table.Column<int>(type: "int", nullable: true),
                    GyroZFeaturesId = table.Column<int>(type: "int", nullable: true),
                    AccXFeaturesId = table.Column<int>(type: "int", nullable: true),
                    AccYFeaturesId = table.Column<int>(type: "int", nullable: true),
                    AccZFeaturesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataStampsStatisticalFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_AccXFeaturesId",
                        column: x => x.AccXFeaturesId,
                        principalTable: "StatisticalFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_AccYFeaturesId",
                        column: x => x.AccYFeaturesId,
                        principalTable: "StatisticalFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_AccZFeaturesId",
                        column: x => x.AccZFeaturesId,
                        principalTable: "StatisticalFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_GyroXFeaturesId",
                        column: x => x.GyroXFeaturesId,
                        principalTable: "StatisticalFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_GyroYFeaturesId",
                        column: x => x.GyroYFeaturesId,
                        principalTable: "StatisticalFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_GyroZFeaturesId",
                        column: x => x.GyroZFeaturesId,
                        principalTable: "StatisticalFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataStampsStatisticalFeatures_AccXFeaturesId",
                table: "DataStampsStatisticalFeatures",
                column: "AccXFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_DataStampsStatisticalFeatures_AccYFeaturesId",
                table: "DataStampsStatisticalFeatures",
                column: "AccYFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_DataStampsStatisticalFeatures_AccZFeaturesId",
                table: "DataStampsStatisticalFeatures",
                column: "AccZFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_DataStampsStatisticalFeatures_GyroXFeaturesId",
                table: "DataStampsStatisticalFeatures",
                column: "GyroXFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_DataStampsStatisticalFeatures_GyroYFeaturesId",
                table: "DataStampsStatisticalFeatures",
                column: "GyroYFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_DataStampsStatisticalFeatures_GyroZFeaturesId",
                table: "DataStampsStatisticalFeatures",
                column: "GyroZFeaturesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataStampsStatisticalFeatures");

            migrationBuilder.DropTable(
                name: "StatisticalFeatures");

            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "DataStamps");
        }
    }
}
