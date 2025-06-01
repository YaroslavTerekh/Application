using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskBooking.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddDeskIdToReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenspaceDesks_Reservations_ReservationId",
                table: "OpenspaceDesks");

            migrationBuilder.DropIndex(
                name: "IX_OpenspaceDesks_ReservationId",
                table: "OpenspaceDesks");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "OpenspaceDesks");

            migrationBuilder.AddColumn<Guid>(
                name: "OpenspaceDeskId",
                table: "Reservations",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpenspaceDeskId",
                table: "Reservations");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "OpenspaceDesks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenspaceDesks_ReservationId",
                table: "OpenspaceDesks",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenspaceDesks_Reservations_ReservationId",
                table: "OpenspaceDesks",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }
    }
}
