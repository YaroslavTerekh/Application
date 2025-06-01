using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskBooking.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRoomAmenityConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomAmenity_Amenities_AmenityId1",
                table: "RoomAmenity");

            migrationBuilder.DropIndex(
                name: "IX_RoomAmenity_AmenityId1",
                table: "RoomAmenity");

            migrationBuilder.DropColumn(
                name: "AmenityId1",
                table: "RoomAmenity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AmenityId1",
                table: "RoomAmenity",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenity_AmenityId1",
                table: "RoomAmenity",
                column: "AmenityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAmenity_Amenities_AmenityId1",
                table: "RoomAmenity",
                column: "AmenityId1",
                principalTable: "Amenities",
                principalColumn: "Id");
        }
    }
}
