using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Application.Services.TaskList.Commands;
using TaskList.Domain.Entities;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.TaskList.CommandHandlers
{
    public sealed class AddTaskListHandler : IRequestHandler<AddTaskListCommandAsync, Guid>
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly IMapper _mapper;

        public AddTaskListHandler(ITaskListRepository taskListRepository, IMapper mapper)
        {
            _taskListRepository = taskListRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddTaskListCommandAsync request, CancellationToken cancellationToken)
        {
            var taskListDto = _mapper.Map<TaskListEntity>(request.TaskListDto);
            taskListDto.Owner = request.Owner;

            var newTaskList = _taskListRepository.Add(taskListDto);
            await _taskListRepository.SaveChangesAsync(cancellationToken);

            return newTaskList.Id;
        }
    }
}
