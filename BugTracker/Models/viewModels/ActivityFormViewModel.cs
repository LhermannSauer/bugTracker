namespace BugTracker.Models.viewModels
{
    public class ActivityFormViewModel
    {
        public SelectList? Priorities { get; set; }

        public IEnumerable<Developer>? ReassignToList { get; set; }

        public SelectList? Statuses { get; set; }

        public Issue? Issue { get; set; }

        public bool IsDeveloper;

        public int? Id { get; set; }

        [Required, MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public int? IssueId { get; set; }

        public string? UserId { get; set; }

        [Required]
        public bool UpdatedStatus { get; set; }

        public int StatusId { get; set; }

        public bool ReassignedIssue { get; set; }

        [DevSelectedIfReassignedChecked]
        public int? ReassignToId { get; set; }

        public bool? IsAutomaticallyGenerated { get; set; }
    }
}