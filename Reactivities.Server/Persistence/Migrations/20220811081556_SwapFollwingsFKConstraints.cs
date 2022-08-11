using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class SwapFollwingsFKConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileFollowings_Profiles_ObserverId",
                table: "ProfileFollowings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileFollowings_Profiles_TargetId",
                table: "ProfileFollowings");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileFollowings_Profiles_ObserverId",
                table: "ProfileFollowings",
                column: "ObserverId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileFollowings_Profiles_TargetId",
                table: "ProfileFollowings",
                column: "TargetId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileFollowings_Profiles_ObserverId",
                table: "ProfileFollowings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileFollowings_Profiles_TargetId",
                table: "ProfileFollowings");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileFollowings_Profiles_ObserverId",
                table: "ProfileFollowings",
                column: "ObserverId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileFollowings_Profiles_TargetId",
                table: "ProfileFollowings",
                column: "TargetId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
