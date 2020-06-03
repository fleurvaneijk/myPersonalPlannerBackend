using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPersonalPlannerBackend.Migrations
{
    public partial class itemuserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "User",
                table: "PlannerItems",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "PlannerItems",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
