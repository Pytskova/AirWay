using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace airlineApp.Migrations
{
    /// <inheritdoc />
    public partial class SyncAfterModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingEvents_Employees_TrainerId",
                table: "TrainingEvents");

            migrationBuilder.DropIndex(
                name: "IX_TrainingEvents_TrainerId",
                table: "TrainingEvents");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "TrainingEvents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "TrainingEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingEvents_TrainerId",
                table: "TrainingEvents",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingEvents_Employees_TrainerId",
                table: "TrainingEvents",
                column: "TrainerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
