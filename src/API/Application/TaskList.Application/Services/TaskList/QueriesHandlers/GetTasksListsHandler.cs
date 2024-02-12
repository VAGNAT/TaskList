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
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.TaskList.QueriesHandlers
{
    public sealed class GetTasksListsHandler : IRequestHandler<GetTaskListsQueryAsync, IEnumerable<TaskListDto>>
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly IMapper _mapper;

        public GetTasksListsHandler(ITaskListRepository taskListRepository, IMapper mapper)
        {
            _taskListRepository = taskListRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TaskListDto>> Handle(GetTaskListsQueryAsync request, CancellationToken cancellationToken)
        {
            var taskLists = await _taskListRepository.GetTaskListsByOwner(request.Owner);

            return taskLists.Select(_mapper.Map<TaskListDto>);
        }
    }
}
