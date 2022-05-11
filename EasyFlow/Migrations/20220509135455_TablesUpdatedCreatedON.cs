using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyFlow.Migrations
{
    public partial class TablesUpdatedCreatedON : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestState",
                table: "PreviousWorker");

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "WorkerReq",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "CompanyReq",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "AdminWorkers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedOn",
                table: "AdminCompanyies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "WorkerReq");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CompanyReq");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AdminWorkers");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AdminCompanyies");

            migrationBuilder.AddColumn<string>(
                name: "RequestState",
                table: "PreviousWorker",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
