using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.Backend.Features.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LegalName_Value = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DefaultCommission_Value = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: true),
                    DefaultCommissionOnExtras_Value = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: true),
                    CommissionMethod = table.Column<int>(type: "int", nullable: false),
                    EmergencyContact_Email_Value = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EmergencyContact_Phone_Value = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name_Value = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContactDetails_Email_Value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContactDetails_Phone_Value = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address_Street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address_ZipCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    AgencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyContacts_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContacts_AgencyId",
                table: "AgencyContacts",
                column: "AgencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgencyContacts");

            migrationBuilder.DropTable(
                name: "Agencies");
        }
    }
}
