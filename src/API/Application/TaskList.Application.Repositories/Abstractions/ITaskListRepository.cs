using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.Entities;

namespace TaskList.Application.Repositories.Abstractions
{
    public interface ITaskListRepository : IRepository<TaskListEntity>
    {
        Task<IEnumerable<TaskListEntity>> GetTaskListsByOwner(string owner);
    }
}
