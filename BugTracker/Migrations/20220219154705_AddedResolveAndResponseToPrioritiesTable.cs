using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Migrations
{
    public partial class AddedResolveAndResponseToPrioritiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DaysUntilOverdue",
                table: "Priorities",
                newName: "RespondWithin");

            migrationBuilder.AddColumn<int>(
                name: "ResolveWithin",
                table: "Priorities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResolveWithin",
                table: "Priorities");

            migrationBuilder.RenameColumn(
                name: "RespondWithin",
                table: "Priorities",
                newName: "DaysUntilOverdue");
        }
    }
}
