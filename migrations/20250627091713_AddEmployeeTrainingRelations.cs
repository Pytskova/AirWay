using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace airlineApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeTrainingRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EmployeeTrainings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "EmployeeTrainings");
        }
    }
}
