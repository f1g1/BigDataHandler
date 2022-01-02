using Microsoft.EntityFrameworkCore.Migrations;

namespace BigDataHandler.Migrations
{
    public partial class AddedActivityStartTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ActivityStartTimestamp",
                table: "DataStampsStatisticalFeatures",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ActivityStartTimestamp",
                table: "DataStamps",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityStartTimestamp",
                table: "DataStampsStatisticalFeatures");

            migrationBuilder.DropColumn(
                name: "ActivityStartTimestamp",
                table: "DataStamps");
        }
    }
}
