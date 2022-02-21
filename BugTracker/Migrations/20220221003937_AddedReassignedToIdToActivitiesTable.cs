using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Migrations
{
    public partial class AddedReassignedToIdToActivitiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<bool>(
                name: "ReassignedIssue",
                table: "Activities",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReassignedToId",
                table: "Activities",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "UpdatedStatus",
                table: "Activities",
                type: "tinyint(1)",
                nullable: true);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "ReassignedIssue",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ReassignedToId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "UpdatedStatus",
                table: "Activities");

        }
    }
}
