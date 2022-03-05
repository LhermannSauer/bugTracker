namespace BugTracker.Models
{
    public class Developer
    {
        public int Id { get; set; }

        public AppUser? User { get; set; }

        public string? UserId { get; set; }

        public IEnumerable<Issue>? Assignments { get; set; }

    }
}