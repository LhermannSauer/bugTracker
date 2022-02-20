using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT into aspnetroles ('Name') VALUES ('CanCloseIssues');
                INSERT into aspnetroles ('Name') VALUES ('CanReassignIssues');
               ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("REMOVE FROM aspnetroles");
        }
    }
}
