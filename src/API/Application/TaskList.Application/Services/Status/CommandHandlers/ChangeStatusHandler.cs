using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Application.Services.Status.Commands;
using TaskList.Domain.Entities;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.Status.CommandHandlers
{
    public sealed class ChangeStatusHandler : IRequestHandler<ChangeStatusCommandAsync, StatusTaskHistoryDto>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IStatusTaskHistoryRepository _statusRepository;
        private readonly IMapper _mapper;

        public ChangeStatusHandler(ITaskRepository taskRepository, IStatusTaskHistoryRepository statusRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<StatusTaskHistoryDto> Handle(ChangeStatusCommandAsync request, CancellationToken cancellationToken)
        {
            _ = await _taskRepository.GetByIdAsync(request.StatusTask.TaskId) ??
                 throw new KeyNotFoundException($"Not found {nameof(TaskDto)} with this id: {request.StatusTask.TaskId}");

            var newStatus = _statusRepository.Add(_mapper.Map<StatusTaskHistory>(request.StatusTask));
            await _statusRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<StatusTaskHistoryDto>(newStatus);
        }
    }
}
