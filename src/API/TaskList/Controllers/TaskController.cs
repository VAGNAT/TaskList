using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using TaskList.Application.Services.Task.CommandHandlers;
using TaskList.Application.Services.Task.Commands;
using TaskList.Application.Services.Task.Queries;
using TaskList.Application.Services.TaskList.Commands;
using TaskList.Application.Services.TaskList.Queries;
using TaskList.Domain.EntitiesDto;
using TaskList.Models.Task;
using TaskList.Models.TaskList;
using TaskList.ResponseModels.Task;
using TaskList.ResponseModels.TaskList;

namespace TaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public TaskController(IMapper mapper, ISender sender)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Uninitialized property");
            _sender = sender ?? throw new ArgumentNullException(nameof(sender), "Uninitialized property");
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get tasks",
            Description = "Get tasks list by lists id",
            Tags = new[] { "Task" }
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "All tasks received", typeof(List<TaskListResponseShort>))]
        public async Task<IActionResult> GetTasks(
            [Required][FromQuery] Guid listId,
            [FromQuery] string? name,
            [FromQuery] string? description,
            [FromQuery] bool sortByAddDate,
            [Required][FromQuery] int itemsPerPage,
            [Required][FromQuery] int page)
        {
            var filterDto = new TaskFilterDto()
            {
                Name = name,
                Description = description,
                SortByAddDate = sortByAddDate,
                ItemsPerPage = itemsPerPage,
                Page = page
            };

            return Ok(_mapper.Map<IEnumerable<TaskResponseShort>>(await _sender.Send(new GetTasksByListIdQueryAsync(listId, filterDto))));
        }

        [HttpGet("{id:Guid}", Name = "GetTaskById")]
        [SwaggerOperation(
            Summary = "Get a task",
            Description = "Get a task by specified id from the database",
            Tags = new[] { "Task" }
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "Received task", typeof(TaskResponseShort))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The task for the specified ID was not found")]
        public async Task<IActionResult> GetTaskById([FromRoute] Guid id)
        {

            var task = await _sender.Send(new GetTaskByIdQueryAsync(id));

            return Ok(_mapper.Map<TaskResponseShort>(task));
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create task",
            Description = "Create a task in the list of tasks specified by id",
            Tags = new[] { "Task" }
            )]
        [SwaggerResponse(StatusCodes.Status201Created, "The task added to the database", typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Required field not specified")]
        public async Task<ActionResult> AddTask([FromBody] TaskCreateModel taskModel)
        {
            var addedTask = await _sender.Send(new AddTaskCommandAsync(_mapper.Map<TaskDto>(taskModel)));

            return CreatedAtRoute(nameof(GetTaskById), new { id = addedTask }, addedTask);
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Updates the task by ID",
            Description = "Updates the task by ID in the database",
            Tags = new[] { "Task" }
            )]
        [SwaggerResponse(StatusCodes.Status202Accepted, "Updated task for the specified ID")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The task for the specified ID was not found")]
        public async Task<ActionResult> UpdateTask([FromBody] TaskUpdateModel taskModel)
        {
            await _sender.Send(new UpdateTaskCommandAsync(_mapper.Map<TaskDto>(taskModel)));

            return AcceptedAtRoute(nameof(GetTaskById), new { taskModel.Id });
        }

        [HttpPost("MoveTask", Name = "MoveTask")]
        [SwaggerOperation(
            Summary = "Move task",
            Description = "Move a task to another task list",
            Tags = new[] { "Task" }
            )]
        [SwaggerResponse(StatusCodes.Status202Accepted, "Moved a task to another task list", typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The task for the specified ID was not found")]
        public async Task<ActionResult> MoveTask([FromBody] TaskMoveModel taskModel)
        {
            await _sender.Send(new MoveTaskCommandAsync(_mapper.Map<TaskMoveDto>(taskModel)));

            return AcceptedAtRoute(nameof(GetTaskById), new { taskModel.Id });
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletes a task by the specified Id",
            Description = "Deletes a task by the specified Id in the database",
            Tags = new[] { "Task" }
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The task with the specified Id has been deleted")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The task for the specified ID was not found")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            await _sender.Send(new DeleteTaskCommandAsync(id));

            return Ok($"Task list with id = {id} has been removed");
        }
    }
}
