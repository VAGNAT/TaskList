using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.Entities;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskList.Infrastructure.Repositories.Implementation
{
    public sealed class TaskListRepository : BaseRepository<TaskListEntity>, ITaskListRepository
    {
        public TaskListRepository(IApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskListEntity>> GetTaskListsByOwner(string owner)
        {
            return await _entitySet.Where(t => t.Owner == owner).AsNoTracking().ToListAsync();
        }

        public override async Task<TaskListEntity?> GetByIdAsync(Guid id, bool noTracking = true)
        {
            return noTracking ? await _entitySet.Include(x=>x.Tasks).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) :
                await _entitySet.Include(x => x.Tasks).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
