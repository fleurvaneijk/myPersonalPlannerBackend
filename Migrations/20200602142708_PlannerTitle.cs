using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPersonalPlannerBackend.Migrations
{
    public partial class PlannerTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Planners",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Planners");
        }
    }
}
