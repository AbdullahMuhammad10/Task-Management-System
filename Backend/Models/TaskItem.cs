using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = String.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
