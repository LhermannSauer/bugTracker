namespace BugTracker.Models
{
    public class AppUser : IdentityUser
    {

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? AvatarUrl { get; set; }


        public AppUser() : base()
        {

        }
    }
}
