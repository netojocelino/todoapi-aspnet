namespace Model
{
    public class Todo {
        public Int64 ID { get; set; }
        public String? Title { get; set; }
        public Boolean IsComplete { get; set; }
        public string? secret { get; set; }
    }
}