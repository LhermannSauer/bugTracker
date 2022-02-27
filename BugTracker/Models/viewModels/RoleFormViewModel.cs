namespace BugTracker.Models.viewModels
{
    public class RoleFormViewModel
    {
        public string? Id { get; set; }

        [Required, MinLength(5), MaxLength(255)]
        public string? Name { get; set; }
    }
}
