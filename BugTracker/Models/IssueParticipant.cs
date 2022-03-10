namespace BugTracker.Models
{
    public class IssueParticipant
    {

        public int IssueId { get; set; }                    // First part of composite PK; PK to Issue
        public string ParticipantsId { get; set; } = null!; // Second  part of composite PK; PK to User

    }
}
