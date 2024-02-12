using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Application.Services.TaskList.Commands;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.TaskList.CommandHandlers
{
    public sealed class DeleteTaskListHandler : IRequestHandler<DeleteTaskListCommandAsync>
    {
        private readonly ITaskListRepository _taskListRepository;

        public DeleteTaskListHandler(ITaskListRepository taskListRepository)
        {
            _taskListRepository = taskListRepository;
        }

        public async System.Threading.Tasks.Task Handle(DeleteTaskListCommandAsync request, CancellationToken cancellationToken)
        {
            var existTaskList = await _taskListRepository.GetByIdAsync(request.Id) ??
                throw new KeyNotFoundException($"Not found {nameof(TaskListDto)} with this id: {request.Id}");

            if (existTaskList.Tasks!.Any())
            {
                throw new InvalidOperationException($"The list of tasks with Id: {request.Id} is not empty");
            }

            _taskListRepository.Delete(existTaskList);
            await _taskListRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
