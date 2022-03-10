
namespace BugTracker.Data.ModelConfigurations
{
    public class IssuesConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            // table

            // primary key

            // property
            builder.Property(i => i.Title).IsRequired().HasMaxLength(50);

            builder.Property(i => i.Description).IsRequired().HasMaxLength(1000);

            builder.Property(i => i.ProjectId).IsRequired();

            builder.Property(i => i.AreaId).IsRequired();

            builder.Property(i => i.PriorityId).IsRequired();

            // relationships

            builder.HasMany(i => i.Participants)
                .WithMany(p => p.IssuesAsParticipant)
                .UsingEntity<IssueParticipant>();
        }
    }
}
