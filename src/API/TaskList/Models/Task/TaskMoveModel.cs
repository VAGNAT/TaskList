using System.ComponentModel.DataAnnotations;

namespace TaskList.Models.Task
{
    public class TaskMoveModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid ToTaskListId { get; set; }
    }
}
