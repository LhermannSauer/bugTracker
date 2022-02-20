namespace BugTracker.Models
{
    public class Status
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //for access in Issues Controller (index)
        public static byte Resolved = 4;
        public static byte Closed = 5;
        public static byte Open = 1;

    }
}