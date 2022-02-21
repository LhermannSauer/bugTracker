namespace BugTracker.Models.viewModels
{
    public class AcitivityFormViewModel
    {
        public SelectList? Priorities { get; set; }

        public IEnumerable<Developer> ReassignToList { get; set; }

        public SelectList? Statuses { get; set; }

        public Issue? Issue { get; set; }

        public bool IsDeveloper;

        [Required]
        public int? Id { get; set; }

        [Required, MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public int? IssueId { get; set; }

        [Required, MaxLength(255)]
        public string? UserId { get; set; }

        [Required]
        public bool UpdatedStatus { get; set; }

        [Required]
        public bool ReassignedIssue { get; set; }

        [Required, MaxLength(255)]
        public string? ReassignToId { get; set; }
    }
}