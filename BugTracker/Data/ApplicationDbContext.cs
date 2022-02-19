using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Issue>? Issues { get; set; }
        public DbSet<Activity>? Activities { get; set; }
        public DbSet<Developer>? Developers { get; set; }
        public DbSet<Area>? Areas { get; set; }
        public DbSet<Priority>? Priorities { get; set; }
        public DbSet<Status>? Statuses { get; set; }
        public DbSet<Project>? Projects { get; set; }



    }
}