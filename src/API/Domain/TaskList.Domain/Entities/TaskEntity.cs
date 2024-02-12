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
    [Table("Task")]
    public class TaskEntity : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public required string Description { get; set; }

        [Required]
        public required DateTime AddDate { get; set; }


        [Required]
        [ForeignKey("TaskList")]
        public Guid TaskListId { get; set; }

        public virtual TaskListEntity? TaskList { get; set; }
    }
}
