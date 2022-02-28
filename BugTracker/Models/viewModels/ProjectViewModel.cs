namespace BugTracker.Models.viewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IdentityUser Manager { get; set; }

        public List<Issue> Issues { get; set; }
    }
}
