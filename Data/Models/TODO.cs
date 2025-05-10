using System.ComponentModel.DataAnnotations;

namespace TODO.Data.Models
{
    public class ToDo
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        public string Description { get; set; }

        public TodoStatus Status { get; set; }

        public Priority Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }

    public enum TodoStatus
    {
        Pending,
        InProgress,
        Completed
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }

}
