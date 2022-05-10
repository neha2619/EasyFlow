using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyFlow.Migrations
{
    public partial class TablesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestState",
                table: "PreviousWorker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestState",
                table: "AdminWorkers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestState",
                table: "AdminCompanyies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestState",
                table: "PreviousWorker");

            migrationBuilder.DropColumn(
                name: "RequestState",
                table: "AdminWorkers");

            migrationBuilder.DropColumn(
                name: "RequestState",
                table: "AdminCompanyies");
        }
    }
}
