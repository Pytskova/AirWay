using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace airlineApp.Migrations
{
    /// <inheritdoc />
    public partial class AddActualTimesToFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActualArrival",
                table: "Flights",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualDeparture",
                table: "Flights",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualArrival",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "ActualDeparture",
                table: "Flights");
        }
    }
}
