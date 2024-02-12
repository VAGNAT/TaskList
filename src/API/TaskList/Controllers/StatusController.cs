using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TaskList.Application.Services.Comment.Commands;
using TaskList.Application.Services.Comment.Queries;
using TaskList.Application.Services.Status.Commands;
using TaskList.Application.Services.Status.Queries;
using TaskList.Application.Services.Task.CommandHandlers;
using TaskList.Application.Services.Task.Commands;
using TaskList.Application.Services.Task.Queries;
using TaskList.Application.Services.TaskList.Commands;
using TaskList.Application.Services.TaskList.Queries;
using TaskList.Domain.EntitiesDto;
using TaskList.Models.Comment;
using TaskList.Models.Status;
using TaskList.Models.Task;
using TaskList.Models.TaskList;
using TaskList.ResponseModels.Comment;
using TaskList.ResponseModels.Status;
using TaskList.ResponseModels.Task;
using TaskList.ResponseModels.TaskList;

namespace TaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public StatusController(IMapper mapper, ISender sender)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Uninitialized property");
            _sender = sender ?? throw new ArgumentNullException(nameof(sender), "Uninitialized property");
        }

        [HttpGet("{taskId:Guid}", Name = "GetStatusById")]
        [SwaggerOperation(
            Summary = "Get a status",
            Description = "Get task status by ID",
            Tags = new[] { "Status" }
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "Received status", typeof(StatusResponseShort))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The task for the specified ID was not found")]
        public async Task<IActionResult> GetStatusById([FromRoute] Guid taskId)
        {
            var task = await _sender.Send(new GetTaskStatusByIdQueryAsync(taskId));

            return Ok(_mapper.Map<StatusResponseShort>(task));
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Change task status",
            Description = "Change task status by task ID",
            Tags = new[] { "Status" }
            )]
        [SwaggerResponse(StatusCodes.Status201Created, "Changed task status by task ID", typeof(StatusResponseShort))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Required field not specified")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The task for the specified ID was not found")]
        public async Task<ActionResult> ChangeStatus([FromBody] StatusChangeModel changeModel)
        {
            var addedStatus = await _sender.Send(new ChangeStatusCommandAsync(_mapper.Map<StatusTaskHistoryDto>(changeModel)));

            return CreatedAtRoute(nameof(GetStatusById), new { taskId = addedStatus.TaskId }, _mapper.Map<StatusResponseShort>(addedStatus));
        }
    }
}
