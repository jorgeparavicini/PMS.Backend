﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.Backend.Core.Migrations
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
                    LegalName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DefaultCommissionRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: true),
                    DefaultCommissionOnExtras = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: true),
                    CommissionMethod = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    EmergencyPhone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmergencyEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
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
                    ContactName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsFrequentVendor = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AgencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgencyContacts_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GroupReservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsQuote = table.Column<bool>(type: "bit", nullable: false),
                    AgencyContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupReservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupReservations_AgencyContacts_AgencyContactId",
                        column: x => x.AgencyContactId,
                        principalTable: "AgencyContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GroupReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_GroupReservations_GroupReservationId",
                        column: x => x.GroupReservationId,
                        principalTable: "GroupReservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckIn = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckOut = table.Column<DateOnly>(type: "date", nullable: false),
                    FolioClosedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationDetails_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContacts_AgencyId",
                table: "AgencyContacts",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupReservations_AgencyContactId",
                table: "GroupReservations",
                column: "AgencyContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetails_ReservationId",
                table: "ReservationDetails",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_GroupReservationId",
                table: "Reservations",
                column: "GroupReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationDetails");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "GroupReservations");

            migrationBuilder.DropTable(
                name: "AgencyContacts");

            migrationBuilder.DropTable(
                name: "Agencies");
        }
    }
}
