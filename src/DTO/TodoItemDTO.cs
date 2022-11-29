using Model;

namespace DTO {
    public class TodoItemDTO {
        public Int64 ID { get; set; }
        public String? Title { get; set; }
        public Boolean IsComplete { get; set; }

        public TodoItemDTO () {}

        public TodoItemDTO (Todo todoItem) {
            this.ID = todoItem.ID;
            this.Title = todoItem.Title;
            this.IsComplete = todoItem.IsComplete;
        }
    }
}