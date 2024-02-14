using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBC_Travel_Group_32.Migrations
{
    /// <inheritdoc />
    public partial class Listings3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ListingId",
                table: "Hotels",
                newName: "HotelId");

            migrationBuilder.RenameColumn(
                name: "ListingId",
                table: "Flights",
                newName: "FlightId");

            migrationBuilder.RenameColumn(
                name: "ListingId",
                table: "CarRentals",
                newName: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HotelId",
                table: "Hotels",
                newName: "ListingId");

            migrationBuilder.RenameColumn(
                name: "FlightId",
                table: "Flights",
                newName: "ListingId");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "CarRentals",
                newName: "ListingId");
        }
    }
}
