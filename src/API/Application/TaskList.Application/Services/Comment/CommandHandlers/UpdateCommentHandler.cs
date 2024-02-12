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
    public sealed class UpdateCommentHandler : IRequestHandler<UpdateCommentCommandAsync>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public UpdateCommentHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async System.Threading.Tasks.Task Handle(UpdateCommentCommandAsync request, CancellationToken cancellationToken)
        {
            var existComment = await _commentRepository.GetByIdAsync(request.Comment.Id, false) ??
                throw new KeyNotFoundException($"Not found {nameof(CommentDto)} with this id: {request.Comment.Id}");

            _mapper.Map(request.Comment, existComment);

            _commentRepository.Update(_mapper.Map<CommentEntity>(existComment));
            await _commentRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
