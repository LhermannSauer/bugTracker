namespace BugTracker.Models.viewModels
{
    public class AssignListViewModel
    {
        public string RoleName { get; set; }
        public IEnumerable<string> UsersToAdd { get; set; }
    }
}
