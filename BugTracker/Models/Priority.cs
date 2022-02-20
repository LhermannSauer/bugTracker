namespace BugTracker.Models
{
    public class Priority
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int RespondWithin { get; set; }

        public int ResolveWithin { get; set; }

        public static int Critical = 1;
        public static int High = 2;
        public static int MediumHigh = 3;
        public static int MediumLow = 4;
        public static int Low = 5;
        public static int Suggestion = 6;
    }
}


