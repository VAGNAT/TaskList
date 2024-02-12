using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Application.Services.Comment.Queries;
using TaskList.Domain.Entities;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.Comment.QueriesHandlers
{
    public sealed class GetCommentsByTaskIdHandler : IRequestHandler<GetCommentsByTaskIdQueryAsync, IEnumerable<CommentDto>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public GetCommentsByTaskIdHandler(ICommentRepository commentRepository, ITaskRepository taskRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentDto>> Handle(GetCommentsByTaskIdQueryAsync request, CancellationToken cancellationToken)
        {
            _ = await _taskRepository.GetByIdAsync(request.TaskId) ??
                throw new KeyNotFoundException($"Not found {nameof(TaskEntity)} with this id: {request.TaskId}");

            var comments = await _commentRepository.GetAllByTaskIdAsync(request.TaskId);

            return _mapper.Map<IEnumerable<CommentDto>>(comments);
        }
    }
}
