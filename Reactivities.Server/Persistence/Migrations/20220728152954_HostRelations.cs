using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class HostRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityUser_Activities_ActivitiesId",
                table: "ActivityUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityUser",
                table: "ActivityUser");

            migrationBuilder.DropIndex(
                name: "IX_ActivityUser_AttendeesId",
                table: "ActivityUser");

            migrationBuilder.RenameColumn(
                name: "ActivitiesId",
                table: "ActivityUser",
                newName: "AttendingActivitiesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityUser",
                table: "ActivityUser",
                columns: new[] { "AttendeesId", "AttendingActivitiesId" });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityUser_AttendingActivitiesId",
                table: "ActivityUser",
                column: "AttendingActivitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityUser_Activities_AttendingActivitiesId",
                table: "ActivityUser",
                column: "AttendingActivitiesId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityUser_Activities_AttendingActivitiesId",
                table: "ActivityUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityUser",
                table: "ActivityUser");

            migrationBuilder.DropIndex(
                name: "IX_ActivityUser_AttendingActivitiesId",
                table: "ActivityUser");

            migrationBuilder.RenameColumn(
                name: "AttendingActivitiesId",
                table: "ActivityUser",
                newName: "ActivitiesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityUser",
                table: "ActivityUser",
                columns: new[] { "ActivitiesId", "AttendeesId" });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityUser_AttendeesId",
                table: "ActivityUser",
                column: "AttendeesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityUser_Activities_ActivitiesId",
                table: "ActivityUser",
                column: "ActivitiesId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
