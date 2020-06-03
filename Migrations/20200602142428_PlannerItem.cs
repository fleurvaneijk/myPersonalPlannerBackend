using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPersonalPlannerBackend.Migrations
{
    public partial class PlannerItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlannerPlannerItems");

            migrationBuilder.AddColumn<int>(
                name: "PlannerId",
                table: "PlannerItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlannerItems_PlannerId",
                table: "PlannerItems",
                column: "PlannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlannerItems_Planners_PlannerId",
                table: "PlannerItems",
                column: "PlannerId",
                principalTable: "Planners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlannerItems_Planners_PlannerId",
                table: "PlannerItems");

            migrationBuilder.DropIndex(
                name: "IX_PlannerItems_PlannerId",
                table: "PlannerItems");

            migrationBuilder.DropColumn(
                name: "PlannerId",
                table: "PlannerItems");

            migrationBuilder.CreateTable(
                name: "PlannerPlannerItems",
                columns: table => new
                {
                    PlannerId = table.Column<int>(type: "int", nullable: false),
                    PlannerItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannerPlannerItems", x => new { x.PlannerId, x.PlannerItemId });
                    table.ForeignKey(
                        name: "FK_PlannerPlannerItems_Planners_PlannerId",
                        column: x => x.PlannerId,
                        principalTable: "Planners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlannerPlannerItems_PlannerItems_PlannerItemId",
                        column: x => x.PlannerItemId,
                        principalTable: "PlannerItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlannerPlannerItems_PlannerItemId",
                table: "PlannerPlannerItems",
                column: "PlannerItemId");
        }
    }
}
