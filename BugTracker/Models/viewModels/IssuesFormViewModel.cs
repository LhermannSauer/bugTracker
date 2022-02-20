namespace BugTracker.Models.viewModels
{
    public class IssuesFormViewModel
    {
        public SelectList? Areas { get; set; }

        public SelectList? Projects { get; set; }

        public SelectList? Priorities { get; set; }

        [Required]
        public int? Id { get; set; }

        [Required, MinLength(5), MaxLength(250)]
        public string? Title { get; set; }

        [Required, MinLength(3), MaxLength(5000)]
        public string? Description { get; set; }

        [Required]
        public int? AreaId { get; set; }

        [Required]
        public int? PriorityId { get; set; }

        [Required]
        public int? ProjectId { get; set; }



        public IssuesFormViewModel()
        {
            Id = 0;
        }
    }
}
