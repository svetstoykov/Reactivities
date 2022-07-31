using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class PictureEntityForProfiels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "Profiles");

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Profiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PictureId",
                table: "Profiles",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Picture_PictureId",
                table: "Profiles",
                column: "PictureId",
                principalTable: "Picture",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Picture_PictureId",
                table: "Profiles");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_PictureId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Profiles");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
