using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyFlow.Migrations
{
    public partial class TimestampTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Timestamps",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Timestamps",
                newName: "id");
        }
    }
}
