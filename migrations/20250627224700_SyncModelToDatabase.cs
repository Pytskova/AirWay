using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace airlineApp.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DelayReason",
                table: "FlightLogs");

            migrationBuilder.DropColumn(
                name: "FuelConsumed",
                table: "FlightLogs");

            migrationBuilder.RenameColumn(
                name: "ActualDeparture",
                table: "FlightLogs",
                newName: "DepartureTime");

            migrationBuilder.RenameColumn(
                name: "ActualArrival",
                table: "FlightLogs",
                newName: "ArrivalTime");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "FlightLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FlightLogId",
                table: "CrewAssignments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CrewAssignments_FlightLogId",
                table: "CrewAssignments",
                column: "FlightLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_CrewAssignments_FlightLogs_FlightLogId",
                table: "CrewAssignments",
                column: "FlightLogId",
                principalTable: "FlightLogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrewAssignments_FlightLogs_FlightLogId",
                table: "CrewAssignments");

            migrationBuilder.DropIndex(
                name: "IX_CrewAssignments_FlightLogId",
                table: "CrewAssignments");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "FlightLogs");

            migrationBuilder.DropColumn(
                name: "FlightLogId",
                table: "CrewAssignments");

            migrationBuilder.RenameColumn(
                name: "DepartureTime",
                table: "FlightLogs",
                newName: "ActualDeparture");

            migrationBuilder.RenameColumn(
                name: "ArrivalTime",
                table: "FlightLogs",
                newName: "ActualArrival");

            migrationBuilder.AddColumn<string>(
                name: "DelayReason",
                table: "FlightLogs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "FuelConsumed",
                table: "FlightLogs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
