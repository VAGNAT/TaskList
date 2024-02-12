using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Application.Services.Status.Queries;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.Status.QueriesHandlers
{
    public sealed class GetTaskStatusByIdHandler : IRequestHandler<GetTaskStatusByIdQueryAsync, StatusTaskHistoryDto>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IStatusTaskHistoryRepository _statusRepository;

        private readonly IMapper _mapper;

        public GetTaskStatusByIdHandler(ITaskRepository taskRepository, IStatusTaskHistoryRepository statusRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<StatusTaskHistoryDto> Handle(GetTaskStatusByIdQueryAsync request, CancellationToken cancellationToken)
        {
            _ = await _taskRepository.GetByIdAsync(request.TaskId) ??
                 throw new KeyNotFoundException($"Not found {nameof(TaskDto)} with this id: {request.TaskId}");

            var status = await _statusRepository.GetStatusByTaskIdAsync(request.TaskId);

            return _mapper.Map<StatusTaskHistoryDto>(status);
        }
    }
}
