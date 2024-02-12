using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.Abstractions;

namespace TaskList.Domain.EntitiesDto
{
    public class TaskDto : BaseEntityDto
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required DateTime AddDate { get; set; }

        public Guid TaskListId { get; set; }

        public required TaskListDto TaskList { get; set; }
    }
}
