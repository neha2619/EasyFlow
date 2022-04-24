using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyFlow.Migrations
{
    public partial class DBCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pass = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    CompanyMail = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CompanyMobile = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CompanyCin = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: true),
                    CompanyGstin = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CompanyPass = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CompanyType = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CompanyState = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CompanyDistrict = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CompanyArea = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CompanySubArea = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    WorkerNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SiteLocation = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    CreatedOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KYCStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "AdminCompanyies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vacancy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminCompanyies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminCompanyies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    WorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkerName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    WorkerMail = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    WorkerMobile = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    WorkerAadhar = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    CreatedOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KYCStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkerPass = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    WorkerType = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    LocationPreference = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: true),
                    companyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.WorkerId);
                    table.ForeignKey(
                        name: "FK_Workers_Companies_companyId",
                        column: x => x.companyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdminWorkers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkerTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminWorkers_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminWorkers_Workers_WorkerTypeId",
                        column: x => x.WorkerTypeId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminCompanyies_CompanyId",
                table: "AdminCompanyies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminWorkers_WorkerId",
                table: "AdminWorkers",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminWorkers_WorkerTypeId",
                table: "AdminWorkers",
                column: "WorkerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_companyId",
                table: "Workers",
                column: "companyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminCompanyies");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AdminWorkers");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
