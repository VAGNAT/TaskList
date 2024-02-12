using System.ComponentModel.DataAnnotations;

namespace TaskList.Models.TaskList
{
    public sealed class TaskListCreateModel
    {
        [Required]
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
