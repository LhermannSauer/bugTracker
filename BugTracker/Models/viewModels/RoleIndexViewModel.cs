namespace BugTracker.Models.viewModels
{
    public class RoleIndexViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }

        public SelectList Users { get; set; }

        public string? AssignToId { get; set; }

    }
}
