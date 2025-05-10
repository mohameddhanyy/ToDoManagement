using System.ComponentModel.DataAnnotations;
using TODO.Data.Models;

namespace TODO.DTOs
{
    public class CreateToDoDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        public string Description { get; set; }

        public TodoStatus Status { get; set; } = TodoStatus.Pending;

        public Priority Priority { get; set; } = Priority.Medium;

        public DateTime? DueDate { get; set; }

    }
}
