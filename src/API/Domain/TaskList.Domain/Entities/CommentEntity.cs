using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.Abstractions;

namespace TaskList.Domain.Entities
{
    [Table("Comment")]
    public class CommentEntity : BaseEntity
    {
        [Required]
        [MaxLength(1000)]
        public required string Content { get; set; }

        [Required]
        [ForeignKey("Task")]
        public Guid TaskID { get; set; }

        public TaskEntity? Task { get; set; }
    }
}
