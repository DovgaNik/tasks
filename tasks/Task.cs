namespace tasks
{
    public class Task
    {
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime Deadline { get; set; }
        public byte Priority { get; set; }
    }
}
