using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Application.Services.Comment.Commands;
using TaskList.Domain.Entities;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.Comment.CommandHandlers
{
    public sealed class AddCommentHandler : IRequestHandler<AddCommentCommandAsync, Guid>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITaskRepository _taskRepository;

        private readonly IMapper _mapper;

        public AddCommentHandler(ICommentRepository commentRepository, ITaskRepository taskRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddCommentCommandAsync request, CancellationToken cancellationToken)
        {
            _ = await _taskRepository.GetByIdAsync(request.Comment.TaskId) ??
                throw new KeyNotFoundException($"Not found {nameof(TaskDto)} with this id: {request.Comment.TaskId}");

            var newComment = _commentRepository.Add(_mapper.Map<CommentEntity>(request.Comment));
            await _commentRepository.SaveChangesAsync(cancellationToken);

            return newComment.Id;
        }
    }
}
