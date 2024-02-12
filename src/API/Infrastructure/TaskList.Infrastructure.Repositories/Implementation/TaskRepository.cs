using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Interfaces;
using TaskList.Application.Repositories;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Domain.Entities;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Infrastructure.Repositories.Implementation
{
    public sealed class TaskRepository : BaseRepository<TaskEntity>, ITaskRepository
    {
        public TaskRepository(IApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskEntity>> GetPagedByListIdAsync(Guid listId, TaskFilterDto filter)
        {
            var query = _entitySet.Where(t => t.TaskList!.Id == listId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(t => string.Equals(t.Name, filter.Name));
            }

            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                query = query.Where(t => t.Description.Contains(filter.Description));
            }

            if (filter.SortByAddDate)
            {
                query = query.OrderBy(x => x.AddDate);
            }

            query = query.Skip((filter.Page - 1) * filter.ItemsPerPage).Take(filter.ItemsPerPage);

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
