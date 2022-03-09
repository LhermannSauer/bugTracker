namespace BugTracker.Models.viewModels
{
    public class RoleDetailsViewModel
    {
        // for display
        public IdentityRole Role { get; set; } = null!;
        public SelectList? UsersInRole { get; set; }
        public SelectList? UsersNotInRole { get; set; }

        // for upating assignments.
        [Required]
        public string RoleName { get; set; }
        public IEnumerable<AppUser>? UsersToAdd { get; set; }
        public IEnumerable<AppUser>? UsersToRemove { get; set; }

    }
}
