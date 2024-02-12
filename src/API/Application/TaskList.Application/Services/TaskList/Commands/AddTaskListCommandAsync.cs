using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.TaskList.Commands
{
    public sealed record AddTaskListCommandAsync(string Owner, TaskListDto TaskListDto) : IRequest<Guid>;
}
