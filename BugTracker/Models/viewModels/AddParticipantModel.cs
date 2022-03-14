namespace BugTracker.Models.viewModels
{
    public class AddParticipantModel
    {
        public string UserName { get; set; } = null!;
        public int IssueId { get; set; }
    }
}
