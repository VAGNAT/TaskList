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
    public sealed class UpdateTaskHandler : IRequestHandler<UpdateTaskCommandAsync>
    {
        private readonly ITaskRepository _taskRepository;

        private readonly IMapper _mapper;

        public UpdateTaskHandler(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task Handle(UpdateTaskCommandAsync request, CancellationToken cancellationToken)
        {
            var existTask = await _taskRepository.GetByIdAsync(request.Task.Id, false) ??
                throw new KeyNotFoundException($"Not found {nameof(TaskDto)} with this id: {request.Task.Id}");

            _mapper.Map(request.Task, existTask);

            _taskRepository.Update(_mapper.Map<TaskEntity>(existTask));
            await _taskRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
