using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.Entities;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Repositories.Abstractions
{
    public interface IStatusTaskHistoryRepository : IRepository<StatusTaskHistory>
    {
        Task<StatusTaskHistory> GetStatusByTaskIdAsync(Guid taskId);
    }
}
