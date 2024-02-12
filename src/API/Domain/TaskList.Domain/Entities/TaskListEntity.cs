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
    [Table("TaskList")]
    public class TaskListEntity : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [EmailAddress]
        public required string Owner { get; set; }

        public virtual List<TaskEntity>? Tasks { get; set; }
    }
}
