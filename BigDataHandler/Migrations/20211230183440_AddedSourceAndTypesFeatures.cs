using Microsoft.EntityFrameworkCore.Migrations;

namespace BigDataHandler.Migrations
{
    public partial class AddedSourceAndTypesFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_AccXFeaturesId",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_AccYFeaturesId",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_AccZFeaturesId",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_GyroXFeaturesId",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_GyroYFeaturesId",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_GyroZFeaturesId",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropIndex(
                name: "IX_DataStampsStatisticalFeatures_AccXFeaturesId",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropIndex(
                name: "IX_DataStampsStatisticalFeatures_AccYFeaturesId",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropColumn(
                name: "AccXFeaturesId",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropColumn(
                name: "AccYFeaturesId",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.RenameColumn(
                name: "GyroZFeaturesId",
                table: "DataStampsStatisticalFeatures",
                newName: "sensorGyroscopeStatisticsId");

            migrationBuilder.RenameColumn(
                name: "GyroYFeaturesId",
                table: "DataStampsStatisticalFeatures",
                newName: "sensorAccelerometerStatisticsId");

            migrationBuilder.RenameColumn(
                name: "GyroXFeaturesId",
                table: "DataStampsStatisticalFeatures",
                newName: "phoneGyroscopeStatisticsId");

            migrationBuilder.RenameColumn(
                name: "AccZFeaturesId",
                table: "DataStampsStatisticalFeatures",
                newName: "phoneAccelerometerStatisticsId");

            migrationBuilder.RenameIndex(
                name: "IX_DataStampsStatisticalFeatures_GyroZFeaturesId",
                table: "DataStampsStatisticalFeatures",
                newName: "IX_DataStampsStatisticalFeatures_sensorGyroscopeStatisticsId");

            migrationBuilder.RenameIndex(
                name: "IX_DataStampsStatisticalFeatures_GyroYFeaturesId",
                table: "DataStampsStatisticalFeatures",
                newName: "IX_DataStampsStatisticalFeatures_sensorAccelerometerStatisticsId");

            migrationBuilder.RenameIndex(
                name: "IX_DataStampsStatisticalFeatures_GyroXFeaturesId",
                table: "DataStampsStatisticalFeatures",
                newName: "IX_DataStampsStatisticalFeatures_phoneGyroscopeStatisticsId");

            migrationBuilder.RenameIndex(
                name: "IX_DataStampsStatisticalFeatures_AccZFeaturesId",
                table: "DataStampsStatisticalFeatures",
                newName: "IX_DataStampsStatisticalFeatures_phoneAccelerometerStatisticsId");

            migrationBuilder.AddColumn<string>(
                name: "StartLocation",
                table: "DataStampsStatisticalFeatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StopLocation",
                table: "DataStampsStatisticalFeatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "stepsCounr",
                table: "DataStampsStatisticalFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CartesianStatisticalFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    xAxisFeaturesId = table.Column<int>(type: "int", nullable: true),
                    yAxisFeaturesId = table.Column<int>(type: "int", nullable: true),
                    zAxisFeaturesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartesianStatisticalFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartesianStatisticalFeatures_StatisticalFeatures_xAxisFeaturesId",
                        column: x => x.xAxisFeaturesId,
                        principalTable: "StatisticalFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartesianStatisticalFeatures_StatisticalFeatures_yAxisFeaturesId",
                        column: x => x.yAxisFeaturesId,
                        principalTable: "StatisticalFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartesianStatisticalFeatures_StatisticalFeatures_zAxisFeaturesId",
                        column: x => x.zAxisFeaturesId,
                        principalTable: "StatisticalFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartesianStatisticalFeatures_xAxisFeaturesId",
                table: "CartesianStatisticalFeatures",
                column: "xAxisFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_CartesianStatisticalFeatures_yAxisFeaturesId",
                table: "CartesianStatisticalFeatures",
                column: "yAxisFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_CartesianStatisticalFeatures_zAxisFeaturesId",
                table: "CartesianStatisticalFeatures",
                column: "zAxisFeaturesId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataStampsStatisticalFeatures_CartesianStatisticalFeatures_phoneAccelerometerStatisticsId",
                table: "DataStampsStatisticalFeatures",
                column: "phoneAccelerometerStatisticsId",
                principalTable: "CartesianStatisticalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataStampsStatisticalFeatures_CartesianStatisticalFeatures_phoneGyroscopeStatisticsId",
                table: "DataStampsStatisticalFeatures",
                column: "phoneGyroscopeStatisticsId",
                principalTable: "CartesianStatisticalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataStampsStatisticalFeatures_CartesianStatisticalFeatures_sensorAccelerometerStatisticsId",
                table: "DataStampsStatisticalFeatures",
                column: "sensorAccelerometerStatisticsId",
                principalTable: "CartesianStatisticalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataStampsStatisticalFeatures_CartesianStatisticalFeatures_sensorGyroscopeStatisticsId",
                table: "DataStampsStatisticalFeatures",
                column: "sensorGyroscopeStatisticsId",
                principalTable: "CartesianStatisticalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataStampsStatisticalFeatures_CartesianStatisticalFeatures_phoneAccelerometerStatisticsId",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_DataStampsStatisticalFeatures_CartesianStatisticalFeatures_phoneGyroscopeStatisticsId",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_DataStampsStatisticalFeatures_CartesianStatisticalFeatures_sensorAccelerometerStatisticsId",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_DataStampsStatisticalFeatures_CartesianStatisticalFeatures_sensorGyroscopeStatisticsId",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropTable(
                name: "CartesianStatisticalFeatures");

            migrationBuilder.DropColumn(
                name: "StartLocation",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropColumn(
                name: "StopLocation",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropColumn(
                name: "stepsCounr",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.RenameColumn(
                name: "sensorGyroscopeStatisticsId",
                table: "DataStampsStatisticalFeatures",
                newName: "GyroZFeaturesId");

            migrationBuilder.RenameColumn(
                name: "sensorAccelerometerStatisticsId",
                table: "DataStampsStatisticalFeatures",
                newName: "GyroYFeaturesId");

            migrationBuilder.RenameColumn(
                name: "phoneGyroscopeStatisticsId",
                table: "DataStampsStatisticalFeatures",
                newName: "GyroXFeaturesId");

            migrationBuilder.RenameColumn(
                name: "phoneAccelerometerStatisticsId",
                table: "DataStampsStatisticalFeatures",
                newName: "AccZFeaturesId");

            migrationBuilder.RenameIndex(
                name: "IX_DataStampsStatisticalFeatures_sensorGyroscopeStatisticsId",
                table: "DataStampsStatisticalFeatures",
                newName: "IX_DataStampsStatisticalFeatures_GyroZFeaturesId");

            migrationBuilder.RenameIndex(
                name: "IX_DataStampsStatisticalFeatures_sensorAccelerometerStatisticsId",
                table: "DataStampsStatisticalFeatures",
                newName: "IX_DataStampsStatisticalFeatures_GyroYFeaturesId");

            migrationBuilder.RenameIndex(
                name: "IX_DataStampsStatisticalFeatures_phoneGyroscopeStatisticsId",
                table: "DataStampsStatisticalFeatures",
                newName: "IX_DataStampsStatisticalFeatures_GyroXFeaturesId");

            migrationBuilder.RenameIndex(
                name: "IX_DataStampsStatisticalFeatures_phoneAccelerometerStatisticsId",
                table: "DataStampsStatisticalFeatures",
                newName: "IX_DataStampsStatisticalFeatures_AccZFeaturesId");

            migrationBuilder.AddColumn<int>(
                name: "AccXFeaturesId",
                table: "DataStampsStatisticalFeatures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccYFeaturesId",
                table: "DataStampsStatisticalFeatures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataStampsStatisticalFeatures_AccXFeaturesId",
                table: "DataStampsStatisticalFeatures",
                column: "AccXFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_DataStampsStatisticalFeatures_AccYFeaturesId",
                table: "DataStampsStatisticalFeatures",
                column: "AccYFeaturesId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_AccXFeaturesId",
                table: "DataStampsStatisticalFeatures",
                column: "AccXFeaturesId",
                principalTable: "StatisticalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_AccYFeaturesId",
                table: "DataStampsStatisticalFeatures",
                column: "AccYFeaturesId",
                principalTable: "StatisticalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_AccZFeaturesId",
                table: "DataStampsStatisticalFeatures",
                column: "AccZFeaturesId",
                principalTable: "StatisticalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_GyroXFeaturesId",
                table: "DataStampsStatisticalFeatures",
                column: "GyroXFeaturesId",
                principalTable: "StatisticalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_GyroYFeaturesId",
                table: "DataStampsStatisticalFeatures",
                column: "GyroYFeaturesId",
                principalTable: "StatisticalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DataStampsStatisticalFeatures_StatisticalFeatures_GyroZFeaturesId",
                table: "DataStampsStatisticalFeatures",
                column: "GyroZFeaturesId",
                principalTable: "StatisticalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
