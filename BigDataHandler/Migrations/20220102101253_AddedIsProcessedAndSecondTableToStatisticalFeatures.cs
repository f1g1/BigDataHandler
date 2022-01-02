using Microsoft.EntityFrameworkCore.Migrations;

namespace BigDataHandler.Migrations
{
    public partial class AddedIsProcessedAndSecondTableToStatisticalFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "DataStampsStatisticalFeatures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsProcessed",
                table: "DataStampsStatisticalFeatures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "DataStampsStatisticalFeatures",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropColumn(
                name: "IsProcessed",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "DataStampsStatisticalFeatures");
        }
    }
}
