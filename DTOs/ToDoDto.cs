using TODO.Data.Models;

namespace TODO.DTOs
{
    public class ToDoDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TodoStatus Status { get; set; }

        public Priority Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

    }
}
