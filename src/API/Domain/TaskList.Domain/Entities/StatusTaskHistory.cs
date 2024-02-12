using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.Abstractions;

namespace TaskList.Domain.Entities
{
    public class StatusTaskHistory : BaseEntity
    {
        [Required]
        [EnumDataType(typeof(StatusTask))]
        [Column(TypeName = "varchar")]
        public StatusTask Status { get; set; }

        [Required]
        public DateTime AddDate { get; set; }

        [Required]
        [ForeignKey("Task")]
        public Guid TaskId { get; set; }
                
        public TaskEntity? Task { get; set; }
    }
}
