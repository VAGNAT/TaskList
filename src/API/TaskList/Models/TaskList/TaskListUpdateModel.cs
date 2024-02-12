using System.ComponentModel.DataAnnotations;

namespace TaskList.Models.TaskList
{
    public sealed class TaskListUpdateModel
    {
        [Required]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
