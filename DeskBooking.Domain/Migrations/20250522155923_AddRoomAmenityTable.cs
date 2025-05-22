using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskBooking.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomAmenityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Rooms_RoomId",
                table: "Amenities");

            migrationBuilder.DropTable(
                name: "OpenspaceDeskReservation");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_RoomId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Amenities");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "OpenspaceDesks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoomAmenity",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    AmenityId = table.Column<Guid>(type: "uuid", nullable: false),
                    AmenityId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAmenity", x => new { x.RoomId, x.AmenityId });
                    table.ForeignKey(
                        name: "FK_RoomAmenity_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomAmenity_Amenities_AmenityId1",
                        column: x => x.AmenityId1,
                        principalTable: "Amenities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoomAmenity_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpenspaceDesks_ReservationId",
                table: "OpenspaceDesks",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenity_AmenityId",
                table: "RoomAmenity",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenity_AmenityId1",
                table: "RoomAmenity",
                column: "AmenityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenspaceDesks_Reservations_ReservationId",
                table: "OpenspaceDesks",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenspaceDesks_Reservations_ReservationId",
                table: "OpenspaceDesks");

            migrationBuilder.DropTable(
                name: "RoomAmenity");

            migrationBuilder.DropIndex(
                name: "IX_OpenspaceDesks_ReservationId",
                table: "OpenspaceDesks");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "OpenspaceDesks");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "Amenities",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OpenspaceDeskReservation",
                columns: table => new
                {
                    OpenspaceDesksId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReservationsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenspaceDeskReservation", x => new { x.OpenspaceDesksId, x.ReservationsId });
                    table.ForeignKey(
                        name: "FK_OpenspaceDeskReservation_OpenspaceDesks_OpenspaceDesksId",
                        column: x => x.OpenspaceDesksId,
                        principalTable: "OpenspaceDesks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpenspaceDeskReservation_Reservations_ReservationsId",
                        column: x => x.ReservationsId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_RoomId",
                table: "Amenities",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenspaceDeskReservation_ReservationsId",
                table: "OpenspaceDeskReservation",
                column: "ReservationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Rooms_RoomId",
                table: "Amenities",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }
    }
}
