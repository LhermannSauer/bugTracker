namespace BugTracker.Models
{
    public class Activity
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public int? IssueId { get; set; }

        public IdentityUser? User { get; set; }

        public string? UserId { get; set; }

        public bool? UpdatedStatus { get; set; }

        public bool? ReassignedIssue { get; set; }

        public int? ReassignedToId { get; set; }
    }
}