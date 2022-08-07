using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddProfileFollowings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfileFollowings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObserverId = table.Column<int>(type: "int", nullable: false),
                    TargetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileFollowings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileFollowings_Profiles_ObserverId",
                        column: x => x.ObserverId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileFollowings_Profiles_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileFollowings_ObserverId",
                table: "ProfileFollowings",
                column: "ObserverId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileFollowings_TargetId",
                table: "ProfileFollowings",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileFollowings_TargetId_ObserverId",
                table: "ProfileFollowings",
                columns: new[] { "TargetId", "ObserverId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileFollowings");
        }
    }
}
