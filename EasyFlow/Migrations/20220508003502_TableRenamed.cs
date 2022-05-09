using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyFlow.Migrations
{
    public partial class TableRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminReq");

            migrationBuilder.DropColumn(
                name: "WorkerState",
                table: "PreviousWorker");

            migrationBuilder.RenameColumn(
                name: "RequestState",
                table: "WorkerReq",
                newName: "RequestStatus");

            migrationBuilder.CreateTable(
                name: "CompanyReq",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyReq", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyReq");

            migrationBuilder.RenameColumn(
                name: "RequestStatus",
                table: "WorkerReq",
                newName: "RequestState");

            migrationBuilder.AddColumn<string>(
                name: "WorkerState",
                table: "PreviousWorker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdminReq",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminReq", x => x.Id);
                });
        }
    }
}
