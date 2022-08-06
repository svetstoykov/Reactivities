using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class UpdateFKDeleteForProfileFollowings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProfileFollowings_TargetId_ObserverId",
                table: "ProfileFollowings");

            migrationBuilder.AlterColumn<int>(
                name: "ObserverId",
                table: "ProfileFollowings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_ProfileFollowings_TargetId_ObserverId",
                table: "ProfileFollowings",
                columns: new[] { "TargetId", "ObserverId" },
                unique: true);
        }
    }
}
