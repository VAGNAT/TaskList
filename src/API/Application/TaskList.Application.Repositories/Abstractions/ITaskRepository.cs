using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.Entities;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Repositories.Abstractions
{
    public interface ITaskRepository : IRepository<TaskEntity>
    {
        Task<IEnumerable<TaskEntity>> GetPagedByListIdAsync(Guid listId, TaskFilterDto filter);
    }
}
