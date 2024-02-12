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
    public sealed class GetTasksByListIdHandler : IRequestHandler<GetTasksByListIdQueryAsync, IEnumerable<TaskDto>>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskListRepository _listRepository;
        private readonly IMapper _mapper;

        public GetTasksByListIdHandler(ITaskRepository taskRepository, ITaskListRepository listRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _listRepository = listRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TaskDto>> Handle(GetTasksByListIdQueryAsync request, CancellationToken cancellationToken)
        {
            _ = await _listRepository.GetByIdAsync(request.ListId) ??
                throw new KeyNotFoundException($"Not found {nameof(TaskListEntity)} with this id: {request.ListId}");

            var tasks = await _taskRepository.GetPagedByListIdAsync(request.ListId, request.Filter);

            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }
    }
}
