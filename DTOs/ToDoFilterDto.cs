using TODO.Data.Models;

namespace TODO.DTOs
{
    public class ToDoFilterDto
    {
        public TodoStatus? Status { get; set; }
        public Priority? Priority { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
