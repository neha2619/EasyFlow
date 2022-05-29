using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyFlow.Migrations
{
    public partial class WOrkerCompanyReqsNameAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "WorkerReq",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyMail",
                table: "WorkerReq",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyMobile",
                table: "WorkerReq",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "WorkerReq",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "CompanyReq",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "CompanyReq",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkerName",
                table: "CompanyReq",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkerType",
                table: "CompanyReq",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "CompanyReq",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "WorkerReq");

            migrationBuilder.DropColumn(
                name: "CompanyMail",
                table: "WorkerReq");

            migrationBuilder.DropColumn(
                name: "CompanyMobile",
                table: "WorkerReq");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "WorkerReq");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "CompanyReq");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "CompanyReq");

            migrationBuilder.DropColumn(
                name: "WorkerName",
                table: "CompanyReq");

            migrationBuilder.DropColumn(
                name: "WorkerType",
                table: "CompanyReq");

            migrationBuilder.DropColumn(
                name: "email",
                table: "CompanyReq");
        }
    }
}
