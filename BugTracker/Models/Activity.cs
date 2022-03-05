namespace BugTracker.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }

        public string? Description { get; set; }

        public int? IssueId { get; set; }

        public AppUser? User { get; set; }

        public string? UserId { get; set; }

        public bool? UpdatedStatus { get; set; }

        public int? StatusId { get; set; }

        public Status? Status { get; set; }

        public bool? ResolvedIssue { get; set; }

        public bool? ReassignedIssue { get; set; }

        public int? ReassignedToId { get; set; }

        public Developer? ReassignedTo { get; set; }

        public bool? IsAutomaticallyGenerated { get; set; }


    }
}