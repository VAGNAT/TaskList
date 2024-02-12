using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TaskList.Application.Services.Comment.Commands;
using TaskList.Application.Services.Comment.Queries;
using TaskList.Application.Services.Task.CommandHandlers;
using TaskList.Application.Services.Task.Commands;
using TaskList.Application.Services.Task.Queries;
using TaskList.Application.Services.TaskList.Commands;
using TaskList.Application.Services.TaskList.Queries;
using TaskList.Domain.EntitiesDto;
using TaskList.Models.Comment;
using TaskList.Models.Task;
using TaskList.Models.TaskList;
using TaskList.ResponseModels.Comment;
using TaskList.ResponseModels.Task;
using TaskList.ResponseModels.TaskList;

namespace TaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public CommentController(IMapper mapper, ISender sender)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Uninitialized property");
            _sender = sender ?? throw new ArgumentNullException(nameof(sender), "Uninitialized property");
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get comments",
            Description = "Get comments list by lists id",
            Tags = new[] { "Comment" }
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "All comments received", typeof(List<CommentResponseShort>))]
        public async Task<IActionResult> GetComments([Required][FromQuery] Guid taskId)
        {
            return Ok(_mapper.Map<IEnumerable<CommentResponseShort>>(await _sender.Send(new GetCommentsByTaskIdQueryAsync(taskId))));
        }

        [HttpGet("{id:Guid}", Name = "GetCommentById")]
        [SwaggerOperation(
            Summary = "Get a comment",
            Description = "Get a comment by specified id from the database",
            Tags = new[] { "Comment" }
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "Received comment", typeof(CommentResponseShort))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The comment for the specified ID was not found")]
        public async Task<IActionResult> GetCommentById([FromRoute] Guid id)
        {

            var task = await _sender.Send(new GetCommentByIdQueryAsync(id));

            return Ok(_mapper.Map<CommentResponseShort>(task));
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create comment",
            Description = "Create a comment in the list of tasks specified by id",
            Tags = new[] { "Comment" }
            )]
        [SwaggerResponse(StatusCodes.Status201Created, "The comment added to the database", typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Required field not specified")]
        public async Task<ActionResult> AddComment([FromBody] CommentCreateModel commentModel)
        {
            var addedComment = await _sender.Send(new AddCommentCommandAsync(_mapper.Map<CommentDto>(commentModel)));

            return CreatedAtRoute(nameof(GetCommentById), new { id = addedComment }, addedComment);
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Updates the comment by ID",
            Description = "Updates the comment by ID in the database",
            Tags = new[] { "Comment" }
            )]
        [SwaggerResponse(StatusCodes.Status202Accepted, "Updated comment for the specified ID")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The comment for the specified ID was not found")]
        public async Task<ActionResult> UpdateComment([FromBody] CommentUpdateModel commentModel)
        {
            await _sender.Send(new UpdateCommentCommandAsync(_mapper.Map<CommentDto>(commentModel)));

            return AcceptedAtRoute(nameof(GetCommentById), new { commentModel.Id });
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletes a comment by the specified Id",
            Description = "Deletes a comment by the specified Id in the database",
            Tags = new[] { "Comment" }
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The comment with the specified Id has been deleted")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The comment for the specified ID was not found")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            await _sender.Send(new DeleteCommentCommandAsync(id));

            return Ok($"Comment with id = {id} has been removed");
        }
    }
}
