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
                    WorkerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "Email", "Mobile", "Name", "Pass" },
                values: new object[] { new Guid("8a6ea087-cbc8-4699-92ce-13056ae550fe"), "Samraiden@gmail.com", "8976543209", "Sam Raiden", "hshskhsk" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "CompanyArea", "CompanyCin", "CompanyDistrict", "CompanyGstin", "CompanyMail", "CompanyMobile", "CompanyName", "CompanyPass", "CompanyState", "CompanySubArea", "CompanyType", "CreatedOn", "KYCStatus", "SiteLocation", "UpdatedOn", "WorkerNumber" },
                values: new object[,]
                {
                    { new Guid("2a5ddaf5-d15b-4976-93a1-41e8f79aeccc"), "Ashiyana", "123456789012345678901", "Lucknow", "123456789012345", "ITsolution@gmail.com", "8318692776", "IT_Solutions Ltd", "fsdgvdsg", "Uttar Pradesh", "Sector o", "Hotel", "24.04.2022", "Yes", "sector o, sector p, sector d,sector f", "12.05.2022", "90" },
                    { new Guid("c86111fd-8827-4435-a601-404f9a213c23"), "HHazratganj", "123456789012345672341", "Lucknow", "123456788792345", "Adminsolution@gmail.com", "8318692776", "Admin_Solutions Ltd", "fsdedgvdsg", "Uttar Pradesh", "Rana Pratap Marg", "Construction", "14.04.2022", "No", "Rana Pratap Marg, sector p, sector d,sector f", "2.05.2022", "90" }
                });

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "WorkerId", "CreatedOn", "KYCStatus", "LocationPreference", "UpdatedOn", "WorkerAadhar", "WorkerMail", "WorkerMobile", "WorkerName", "WorkerPass", "WorkerType", "companyId" },
                values: new object[,]
                {
                    { new Guid("46a32c93-1663-447f-8801-db7db9c42719"), "24.04.2022", "Yes", "Alambag,charbag", "12.05.2022", "123456789012", "Samraiden@gmail.com", "8976543209", "Sam Raiden", "hjhfdjgjgg", "Plumber", null },
                    { new Guid("76ed4298-c549-465b-af8f-a80abae08616"), "21.04.2022", "No", "Hazaratganj,Aashiyana", "11.05.2022", "120000789012", "Ronraiden@gmail.com", "8976786209", "Ron Raiden", "hjhfdjsdsdgjgg", "Carpenter", null }
                });

            migrationBuilder.InsertData(
                table: "AdminCompanyies",
                columns: new[] { "Id", "CompanyId", "Location", "Vacancy", "WorkerType" },
                values: new object[] { new Guid("a3347540-c60d-45f4-8ade-af0a189397a3"), new Guid("2a5ddaf5-d15b-4976-93a1-41e8f79aeccc"), "sector o", "5", "Plumber" });

            migrationBuilder.InsertData(
                table: "AdminCompanyies",
                columns: new[] { "Id", "CompanyId", "Location", "Vacancy", "WorkerType" },
                values: new object[] { new Guid("76c00047-4526-48a9-9947-ea2be4133e64"), new Guid("c86111fd-8827-4435-a601-404f9a213c23"), "sector p", "7", "Carpenter" });

            migrationBuilder.InsertData(
                table: "AdminWorkers",
                columns: new[] { "Id", "Location", "WorkerId", "WorkerType" },
                values: new object[] { new Guid("2d1abe8a-a2df-4394-a7c2-be8dad068519"), "Hazaratganj", new Guid("76ed4298-c549-465b-af8f-a80abae08616"), "Carpenter" });

            migrationBuilder.CreateIndex(
                name: "IX_AdminCompanyies_CompanyId",
                table: "AdminCompanyies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminWorkers_WorkerId",
                table: "AdminWorkers",
                column: "WorkerId");

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
