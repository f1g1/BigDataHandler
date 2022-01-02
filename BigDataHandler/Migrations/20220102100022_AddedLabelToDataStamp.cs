using Microsoft.EntityFrameworkCore.Migrations;

namespace BigDataHandler.Migrations
{
    public partial class AddedLabelToDataStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "stepsCounr",
                table: "DataStampsStatisticalFeatures",
                newName: "stepsCount");

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "DataStamps",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Label",
                table: "DataStamps");

            migrationBuilder.RenameColumn(
                name: "stepsCount",
                table: "DataStampsStatisticalFeatures",
                newName: "stepsCounr");
        }
    }
}
