using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class RevertChangesForObserverIdAndIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProfileFollowings_ObserverId",
                table: "ProfileFollowings");

            migrationBuilder.AlterColumn<int>(
                name: "ObserverId",
                table: "ProfileFollowings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileFollowings_ObserverId_TargetId",
                table: "ProfileFollowings",
                columns: new[] { "ObserverId", "TargetId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProfileFollowings_ObserverId_TargetId",
                table: "ProfileFollowings");

            migrationBuilder.AlterColumn<int>(
                name: "ObserverId",
                table: "ProfileFollowings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileFollowings_ObserverId",
                table: "ProfileFollowings",
                column: "ObserverId");
        }
    }
}
