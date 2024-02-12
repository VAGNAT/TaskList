using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Application.Services.Task.Commands;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.Task.CommandHandlers
{
    public sealed class DeleteTaskHandler : IRequestHandler<DeleteTaskCommandAsync>
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async System.Threading.Tasks.Task Handle(DeleteTaskCommandAsync request, CancellationToken cancellationToken)
        {
            var existTask = await _taskRepository.GetByIdAsync(request.Id) ??
                throw new KeyNotFoundException($"Not found {nameof(TaskListDto)} with this id: {request.Id}");

            _taskRepository.Delete(existTask);
            await _taskRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
