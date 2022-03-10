namespace BugTracker.Models
{
    public class AppUser : IdentityUser
    {

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? AvatarUrl { get; set; }

        public Position UserPosition { get; set; }

        public IEnumerable<Issue> IssuesAsParticipant { get; set; }

        public IEnumerable<Issue> IssuesCreated { get; set; }

        public AppUser() : base()
        {

        }

        public enum Position
        {
            Administrator,
            Manager,
            Developer,
            User,
        }


    }
}
