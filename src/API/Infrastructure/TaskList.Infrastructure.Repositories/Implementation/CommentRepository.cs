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
    public class CommentRepository : BaseRepository<CommentEntity>, ICommentRepository
    {
        public CommentRepository(IApplicationContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CommentEntity>> GetAllByTaskIdAsync(Guid taskId)
        {
            return await _entitySet.Where(c => c.TaskID == taskId).AsNoTracking().ToListAsync();
        }
    }
}
