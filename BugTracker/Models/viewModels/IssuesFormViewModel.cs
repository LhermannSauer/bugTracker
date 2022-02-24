namespace BugTracker.Models.viewModels
{
    public class IssuesFormViewModel
    {
        public SelectList? Areas { get; set; }

        public SelectList? Projects { get; set; }

        public SelectList? Priorities { get; set; }

        [Required]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please give a title to the issue."), MinLength(5), MaxLength(250)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Please provide a description for this issue."), MinLength(3), MaxLength(5000)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please indicate to which area this issue is related.")]
        public int? AreaId { get; set; }

        [Required(ErrorMessage = "Please indicate the priority of this issue.")]
        public int? PriorityId { get; set; }

        [Required(ErrorMessage = "Please indicate which project has this issue.")]
        public int? ProjectId { get; set; }


        public IssuesFormViewModel()
        {
            Id = 0;
        }
    }
}
