using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.Abstractions;
using TaskList.Domain.Entities;

namespace TaskList.Domain.EntitiesDto
{
    public class StatusTaskHistoryDto : BaseEntityDto
    {
        public StatusTask Status { get; set; }

        public DateTime AddDate { get; set; }

        public Guid TaskId { get; set; }

        public required TaskDto Task { get; set; }
    }
}
