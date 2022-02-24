using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Migrations
{
    public partial class AddedNavigationToActivitiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Developers",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ReassignedToId",
                table: "Activities",
                column: "ReassignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_StatusId",
                table: "Activities",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Developers_ReassignedToId",
                table: "Activities",
                column: "ReassignedToId",
                principalTable: "Developers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Statuses_StatusId",
                table: "Activities",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Developers_ReassignedToId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Statuses_StatusId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_ReassignedToId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_StatusId",
                table: "Activities");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "UserId",
                keyValue: null,
                column: "UserId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Developers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");


        }
    }
}
