namespace BugTracker.Models
{
    public class Priority
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int RespondWithin { get; set; }

        public int ResolveWithin { get; set; }
    }
}