using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.Task.Commands
{
    public sealed record MoveTaskCommandAsync(TaskMoveDto TaskMove) : IRequest;
}
