using System.ComponentModel.DataAnnotations;

namespace TaskPulse.Model;

public class TaskItem
{
    [Key]
    public int Id { get; set; }
    [StringLength(40, ErrorMessage = "The title must be between 4 and 40 characters long.", MinimumLength = 4)]
    public required string Title { get; set; } = string.Empty;
    [StringLength(200, ErrorMessage = "The description must be between 4 and 200 characters long.", MinimumLength = 4)]
    public required string Note { get; set; } = string.Empty;
    public bool IsDone { get; set; } = false;
}

