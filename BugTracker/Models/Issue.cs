

namespace BugTracker.Models
{
    public class Issue
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public Status? Status { get; set; }

        public int? StatusId { get; set; }

        public Area? Area { get; set; }

        public int? AreaId { get; set; }

        public Priority? Priority { get; set; }

        public int? PriorityId { get; set; }

        public Project? Project { get; set; }

        public int? ProjectId { get; set; }

        public IEnumerable<Activity>? Activities { get; set; }

        public IdentityUser? Creator { get; set; }

        public string? CreatorId { get; set; }

        public Developer? AssignedTo { get; set; }

        [Display(Name = "Created")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Updated")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Resolved")]
        public DateTime? ResolvedDate { get; set; }

        // To implement: NotifyUsers
    }
}
