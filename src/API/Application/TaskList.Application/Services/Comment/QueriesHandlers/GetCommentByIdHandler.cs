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
    public sealed class GetCommentByIdHandler : IRequestHandler<GetCommentByIdQueryAsync, CommentDto>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetCommentByIdHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<CommentDto> Handle(GetCommentByIdQueryAsync request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetByIdAsync(request.Id) ??
                 throw new KeyNotFoundException($"Not found {nameof(CommentDto)} with this id: {request.Id}");

            return _mapper.Map<CommentDto>(comment);
        }
    }
}
