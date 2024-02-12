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
    public class UpdateTaskListHandler : IRequestHandler<UpdateTaskListCommandAsync>
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly IMapper _mapper;

        public UpdateTaskListHandler(ITaskListRepository taskListRepository, IMapper mapper)
        {
            _taskListRepository = taskListRepository;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task Handle(UpdateTaskListCommandAsync request, CancellationToken cancellationToken)
        {
            var existTaskList = await _taskListRepository.GetByIdAsync(request.TaskList.Id, false) ??
                throw new KeyNotFoundException($"Not found {nameof(TaskListDto)} with this id: {request.TaskList.Id}");

            _mapper.Map(request.TaskList, existTaskList);

            _taskListRepository.Update(_mapper.Map<TaskListEntity>(existTaskList));
            await _taskListRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
