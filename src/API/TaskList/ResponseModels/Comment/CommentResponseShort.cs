using System.ComponentModel.DataAnnotations;

namespace TaskList.ResponseModels.Comment
{
    public record CommentResponseShort(Guid Id, string Content, Guid TaskId);
}
