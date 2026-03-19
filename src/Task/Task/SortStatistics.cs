namespace Task
{
    public class SortStatistics
    {
        public int Comparisons { get; set; } = 0;
        public int Copies { get; set; } = 0;
        public int RecursiveCalls { get; set; } = 0;
        public TimeSpan ExecTime { get; set; }

        public void Reset()
        {
            Comparisons = 0;
            Copies = 0;
            RecursiveCalls = 0;
            ExecTime = TimeSpan.Zero;
        }
    }
}
