using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Application.Services.Comment.Commands
{
    public sealed record DeleteCommentCommandAsync(Guid Id) : IRequest;
}
