using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.Backend.Core.Migrations
{
    public partial class RemoveCustomUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d85cf649-fecf-49f5-aedb-bb09fec754f1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "784e3e21-8044-4aea-a249-a35ff514b272", "c4bc712e-3f7d-4037-a3b4-eb0cf616235d", "admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "784e3e21-8044-4aea-a249-a35ff514b272");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d85cf649-fecf-49f5-aedb-bb09fec754f1", "836fa78a-cee5-4f95-915d-711c24ee4ef9", "admin", "ADMIN" });
        }
    }
}
