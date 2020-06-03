using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPersonalPlannerBackend.Migrations
{
    public partial class all : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Planners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Owner = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    AgendaLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlannerItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlannerId = table.Column<int>(nullable: false),
                    User = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDone = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannerItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlannerItems_Planners_PlannerId",
                        column: x => x.PlannerId,
                        principalTable: "Planners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlannerUsers",
                columns: table => new
                {
                    PlannerId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannerUsers", x => new { x.PlannerId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PlannerUsers_Planners_PlannerId",
                        column: x => x.PlannerId,
                        principalTable: "Planners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlannerUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlannerItems_Id",
                table: "PlannerItems",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlannerItems_PlannerId",
                table: "PlannerItems",
                column: "PlannerId");

            migrationBuilder.CreateIndex(
                name: "IX_Planners_Id",
                table: "Planners",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlannerUsers_UserId",
                table: "PlannerUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlannerItems");

            migrationBuilder.DropTable(
                name: "PlannerUsers");

            migrationBuilder.DropTable(
                name: "Planners");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
