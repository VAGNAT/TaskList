using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Domain.EntitiesDto
{
    public class TaskFilterDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }
        
        public bool SortByAddDate { get; set; }

        public int ItemsPerPage { get; set; }

        public int Page { get; set; }
    }
}
