using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Interfaces;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Domain.Entities;

namespace TaskList.Infrastructure.Repositories.Implementation
{
    public sealed class StatusTaskHistoryRepository : BaseRepository<StatusTaskHistory>, IStatusTaskHistoryRepository
    {
        public StatusTaskHistoryRepository(IApplicationContext context) : base(context)
        {
        }

        public async Task<StatusTaskHistory> GetStatusByTaskIdAsync(Guid taskId)
        {
            return await _entitySet.Where(s => s.TaskId == taskId).OrderBy(s => s.AddDate).LastAsync();
        }
    }
}
