using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Application.Services.TaskList.Commands
{
    public sealed record DeleteTaskListCommandAsync(Guid Id) : IRequest;
}
