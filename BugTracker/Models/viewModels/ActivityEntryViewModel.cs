namespace BugTracker.Models.viewModels
{
    public class ActivityEntryViewModel
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

        public string? CreatorId { get; set; }

        public bool? IsAutomaticallyGenerated { get; set; }


        public ActivityEntryViewModel(Activity activity)
        {
            Id = activity.Id;

            DateCreated = activity.DateCreated;
            Description = activity.Description;
            IssueId = activity.IssueId;
            User = activity.User;
            UserId = activity.UserId;
            UpdatedStatus = activity.UpdatedStatus;
            StatusId = activity.StatusId;
            Status = activity.Status;
            ResolvedIssue = activity.ResolvedIssue;
            ReassignedIssue = activity.ReassignedIssue;
            ReassignedToId = activity.ReassignedToId;
            ReassignedTo = activity.ReassignedTo;
            IsAutomaticallyGenerated = activity.IsAutomaticallyGenerated;

        }
    }
}
