using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Migrations
{
    public partial class PopulateProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Projects", "Name", "Vidly");
            migrationBuilder.InsertData("Projects", "Name", "MediaSpot");
            migrationBuilder.InsertData("Projects", "Name", "TheFantasticApp");
            migrationBuilder.InsertData("Projects", "Name", "AmazingAPI");
            migrationBuilder.InsertData("Projects", "Name", "Just Another Blogging App");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Projects");
        }
    }
}
