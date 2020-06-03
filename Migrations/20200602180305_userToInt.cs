using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPersonalPlannerBackend.Migrations
{
    public partial class userToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isDone",
                table: "PlannerItems",
                newName: "IsDone");

            migrationBuilder.AlterColumn<int>(
                name: "Owner",
                table: "Planners",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDone",
                table: "PlannerItems",
                newName: "isDone");

            migrationBuilder.AlterColumn<string>(
                name: "Owner",
                table: "Planners",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
