using TaskList.Domain.Abstractions;
using TaskList.Domain.Entities;

namespace TaskList.Domain.EntitiesDto
{
    public class CommentDto : BaseEntityDto
    {
        public required string Content { get; set; }

        public Guid TaskId { get; set; }

        public required TaskEntity Task { get; set; }
    }
}
