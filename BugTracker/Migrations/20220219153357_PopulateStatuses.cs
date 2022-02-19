using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Migrations
{
    public partial class PopulateStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Statuses", "Name", "Open");
            migrationBuilder.InsertData("Statuses", "Name", "Pending Action - Developer");
            migrationBuilder.InsertData("Statuses", "Name", "Pending Action - Requestor");
            migrationBuilder.InsertData("Statuses", "Name", "Resolved");
            migrationBuilder.InsertData("Statuses", "Name", "Closed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM statuses;");
        }
    }
}
