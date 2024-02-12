using System.ComponentModel.DataAnnotations;

namespace TaskList.Models.Task
{
    public class TaskCreateModel
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public Guid TaskListId { get; set; }
    }
}
