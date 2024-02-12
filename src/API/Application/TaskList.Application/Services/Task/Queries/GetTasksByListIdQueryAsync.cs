using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.Task.Queries
{
    public sealed record GetTasksByListIdQueryAsync(Guid ListId, TaskFilterDto Filter) : IRequest<IEnumerable<TaskDto>>;
}
