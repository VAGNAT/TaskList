using System.ComponentModel.DataAnnotations;

namespace TaskList.Models.Task
{
    public class TaskUpdateModel
    {
        [Required]
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
