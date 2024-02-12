using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.Abstractions;
using TaskList.Domain.Entities;

namespace TaskList.Domain.EntitiesDto
{
    public class TaskListDto : BaseEntityDto
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public required string Owner { get; set; }

        public List<TaskEntity>? Tasks { get; set; }
    }
}
