using System.ComponentModel.DataAnnotations;

namespace TaskList.Models.Comment
{
    public class CommentCreateModel
    {
        [Required]
        public required string Content { get; set; }

        [Required]
        public Guid TaskId { get; set; }
    }
}
