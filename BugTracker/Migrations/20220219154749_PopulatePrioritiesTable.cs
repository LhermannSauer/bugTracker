using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTracker.Migrations
{
    public partial class PopulatePrioritiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO priorities(Name, RespondWithin, ResolveWithin)  values('Critical', 1, 3)");
            migrationBuilder.Sql("INSERT INTO priorities(Name, RespondWithin, ResolveWithin)  values('High', 2, 8)");
            migrationBuilder.Sql("INSERT INTO priorities(Name, RespondWithin, ResolveWithin)  values('Medium', 4, 24)");
            migrationBuilder.Sql("INSERT INTO priorities(Name, RespondWithin, ResolveWithin)  values('Low', 7, 48)");
            migrationBuilder.Sql("INSERT INTO priorities(Name, RespondWithin, ResolveWithin)  values('Suggestion', 24, 336)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM priorities");
        }
    }
}
