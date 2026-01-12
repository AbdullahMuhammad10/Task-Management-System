using System.ComponentModel.DataAnnotations;

namespace Backend.Dtos
{
    public class AddTaskDto
    {
        [Required(AllowEmptyStrings = false)] // Title Is Required And Cannot Be An Empty String 
        [MinLength(3,ErrorMessage = "Title Must Be At Least 3 Characters")] // Add Minimum Length Validation As Extra Validation
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
