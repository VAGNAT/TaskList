using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Application.Services.Task.Commands;
using TaskList.Domain.Abstractions;
using TaskList.Domain.Entities;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.Task.CommandHandlers
{
    public sealed class AddTaskHandler : IRequestHandler<AddTaskCommandAsync, Guid>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskListRepository _taskListRepository;
        private readonly IStatusTaskHistoryRepository _statusTaskHistoryRepository;

        private readonly IMapper _mapper;

        public AddTaskHandler(
            ITaskRepository taskRepository,
            ITaskListRepository taskListRepository,
            IStatusTaskHistoryRepository statusTaskHistoryRepository,
            IMapper mapper)
        {
            _taskRepository = taskRepository;
            _taskListRepository = taskListRepository;
            _statusTaskHistoryRepository = statusTaskHistoryRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddTaskCommandAsync request, CancellationToken cancellationToken)
        {
            _ = await _taskListRepository.GetByIdAsync(request.Task.TaskListId) ??
                throw new KeyNotFoundException($"Not found {nameof(TaskListDto)} with this id: {request.Task.TaskListId}");

            request.Task.AddDate = DateTime.UtcNow;

            var newTask = _taskRepository.Add(_mapper.Map<TaskEntity>(request.Task));
            _statusTaskHistoryRepository.Add(new StatusTaskHistory() { Status = StatusTask.Waiting, AddDate = DateTime.UtcNow, TaskId = newTask.Id });

            await _taskRepository.SaveChangesAsync(cancellationToken);

            return newTask.Id;
        }
    }
}
