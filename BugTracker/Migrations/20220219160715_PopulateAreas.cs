using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Migrations
{
    public partial class PopulateAreas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Areas", "Name", "Databases");
            migrationBuilder.InsertData("Areas", "Name", "Backend");
            migrationBuilder.InsertData("Areas", "Name", "Authorization");
            migrationBuilder.InsertData("Areas", "Name", "Registration");
            migrationBuilder.InsertData("Areas", "Name", "Frontend");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM areas");
        }
    }
}
