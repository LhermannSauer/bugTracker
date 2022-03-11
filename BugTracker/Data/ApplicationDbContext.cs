using BugTracker.Data.ModelConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BugTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole, string>
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
        public DbSet<IssueParticipant>? IssueParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            new IssuesConfiguration().Configure(builder.Entity<Issue>());



            builder.Entity<AppUser>()
                .HasMany(u => u.IssuesCreated)
                .WithOne(i => i.Creator);

        }

    }
}