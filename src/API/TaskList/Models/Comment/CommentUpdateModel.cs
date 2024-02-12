using System.ComponentModel.DataAnnotations;

namespace TaskList.Models.Comment
{
    public class CommentUpdateModel
    {
        [Required]
        public Guid Id { get; set; }

        public string? Content { get; set; }
    }
}
