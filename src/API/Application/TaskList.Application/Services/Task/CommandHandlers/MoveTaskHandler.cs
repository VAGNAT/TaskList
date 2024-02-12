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
    public sealed class MoveTaskHandler : IRequestHandler<MoveTaskCommandAsync>
    {
        private readonly ITaskRepository _taskRepository;

        public MoveTaskHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async System.Threading.Tasks.Task Handle(MoveTaskCommandAsync request, CancellationToken cancellationToken)
        {
            var existTaskList = await _taskRepository.GetByIdAsync(request.TaskMove.Id, false) ??
                throw new KeyNotFoundException($"Not found {nameof(TaskDto)} with this id: {request.TaskMove.Id}");

            existTaskList.TaskListId = request.TaskMove.ToTaskListId;

            _taskRepository.Update(existTaskList);
            await _taskRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
