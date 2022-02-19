
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

            builder.Property(i => i.CreatedDate).HasColumnType("Date");

            builder.Property(i => i.UpdatedDate).HasColumnType("Date");

            builder.Property(i => i.UpdatedDate).HasColumnType("Date");





            // relationships
        }
    }
}
