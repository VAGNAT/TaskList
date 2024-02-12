using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Application.Services.Comment.Commands;
using TaskList.Domain.EntitiesDto;

namespace TaskList.Application.Services.Comment.CommandHandlers
{
    public sealed class DeleteCommentHandler : IRequestHandler<DeleteCommentCommandAsync>
    {
        private readonly ICommentRepository _commentRepository;
        public DeleteCommentHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async System.Threading.Tasks.Task Handle(DeleteCommentCommandAsync request, CancellationToken cancellationToken)
        {
            var existComment = await _commentRepository.GetByIdAsync(request.Id) ??
                throw new KeyNotFoundException($"Not found {nameof(CommentDto)} with this id: {request.Id}");

            _commentRepository.Delete(existComment);
            await _commentRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
