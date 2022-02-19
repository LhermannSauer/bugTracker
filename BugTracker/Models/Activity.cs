namespace BugTracker.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public int IssueId { get; set; }

        public IdentityUser? User { get; set; }

    }
}