using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Migrations
{
    public partial class RemovedRelationshipDeveloperActivities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Developers_DeveloperId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_DeveloperId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "Activities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DeveloperId",
                table: "Activities",
                column: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Developers_DeveloperId",
                table: "Activities",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id");
        }
    }
}
