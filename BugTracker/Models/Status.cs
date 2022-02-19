namespace BugTracker.Models
{
    public class Status
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //for access in Issues Controller (index)
        public byte Resolved = 4;
        public byte Closed = 5;
    }
}