using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Application.Services.Task.Queries;
using TaskList.Domain.Entities;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.Task.QueriesHandlers
{
    public sealed class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQueryAsync, TaskDto>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public GetTaskByIdHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }
        public async Task<TaskDto> Handle(GetTaskByIdQueryAsync request, CancellationToken cancellationToken)
        {
            var taskList = await _taskRepository.GetByIdAsync(request.Id) ??
                 throw new KeyNotFoundException($"Not found {nameof(TaskEntity)} with this id: {request.Id}");

            return _mapper.Map<TaskDto>(taskList);
        }
    }
}
