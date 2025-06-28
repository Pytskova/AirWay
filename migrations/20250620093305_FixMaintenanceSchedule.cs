using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace airlineApp.Migrations
{
    /// <inheritdoc />
    public partial class FixMaintenanceSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaintenanceType",
                table: "MaintenanceSchedules");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "MaintenanceSchedules");

            migrationBuilder.RenameColumn(
                name: "ScheduledDate",
                table: "MaintenanceSchedules",
                newName: "MaintenanceDate");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "MaintenanceSchedules",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "SpareParts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AircraftRegistration",
                table: "MaintenanceSchedules",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MaintenanceSchedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "MaintenanceSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_EmployeeId",
                table: "SpareParts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceSchedules_EmployeeId",
                table: "MaintenanceSchedules",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceSchedules_Employees_EmployeeId",
                table: "MaintenanceSchedules",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpareParts_Employees_EmployeeId",
                table: "SpareParts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceSchedules_Employees_EmployeeId",
                table: "MaintenanceSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SpareParts_Employees_EmployeeId",
                table: "SpareParts");

            migrationBuilder.DropIndex(
                name: "IX_SpareParts_EmployeeId",
                table: "SpareParts");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceSchedules_EmployeeId",
                table: "MaintenanceSchedules");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "SpareParts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "MaintenanceSchedules");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "MaintenanceSchedules");

            migrationBuilder.RenameColumn(
                name: "MaintenanceDate",
                table: "MaintenanceSchedules",
                newName: "ScheduledDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MaintenanceSchedules",
                newName: "ScheduleId");

            migrationBuilder.AlterColumn<string>(
                name: "AircraftRegistration",
                table: "MaintenanceSchedules",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "MaintenanceType",
                table: "MaintenanceSchedules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "MaintenanceSchedules",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
