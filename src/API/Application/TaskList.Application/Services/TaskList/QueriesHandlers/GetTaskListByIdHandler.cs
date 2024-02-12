using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Application.Services.TaskList.Queries;
using TaskList.Domain.Entities;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.TaskList.QueriesHandlers
{
    public sealed class GetTaskListByIdHandler : IRequestHandler<GetTaskListByIdQueryAsync, TaskListDto>
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly IMapper _mapper;

        public GetTaskListByIdHandler(ITaskListRepository taskListRepository, IMapper mapper)
        {
            _taskListRepository = taskListRepository;
            _mapper = mapper;
        }
        public async Task<TaskListDto> Handle(GetTaskListByIdQueryAsync request, CancellationToken cancellationToken)
        {
            var taskList = await _taskListRepository.GetByIdAsync(request.Id) ??
                 throw new KeyNotFoundException($"Not found {nameof(TaskListEntity)} with this id: {request.Id}");

            return _mapper.Map<TaskListDto>(taskList);
        }
    }
}
