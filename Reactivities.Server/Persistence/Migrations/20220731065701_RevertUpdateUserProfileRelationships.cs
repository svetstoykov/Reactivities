using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class RevertUpdateUserProfileRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_AspNetUsers_Email",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_AspNetUsers_UserName",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_Email",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserName",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Profiles");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Profiles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Profiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_Email",
                table: "Profiles",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserName",
                table: "Profiles",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_AspNetUsers_Email",
                table: "Profiles",
                column: "Email",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_AspNetUsers_UserName",
                table: "Profiles",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
