using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection;
using TaskList.Application.Services.TaskList.Commands;
using TaskList.Application.Services.TaskList.Queries;
using TaskList.Domain.EntitiesDto;
using TaskList.Models.TaskList;
using TaskList.ResponseModels.TaskList;

namespace TaskList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public TaskListsController(IMapper mapper, ISender sender)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Uninitialized property");
            _sender = sender ?? throw new ArgumentNullException(nameof(sender), "Uninitialized property");
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get task lists",
            Description = "Get task lists of the current user",
            Tags = new[] { "Task list" }
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "All task lists received", typeof(List<TaskListResponseShort>))]
        public async Task<IActionResult> GetTaskLists()
        {
            return Ok(_mapper.Map<IEnumerable<TaskListResponseShort>>(await _sender.Send(new GetTaskListsQueryAsync(GetOwner()))));
        }

        [HttpGet("{id:Guid}", Name = "GetTaskListById")]
        [SwaggerOperation(
            Summary = "Get a list of tasks",
            Description = "Get a list of tasks by specified id from the database",
            Tags = new[] { "Task list" }
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "Received a list of tasks", typeof(TaskListResponseShort))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The list of tasks for the specified ID was not found")]
        public async Task<IActionResult> GetTaskListById([FromRoute] Guid id)
        {

            var taskList = await _sender.Send(new GetTaskListByIdQueryAsync(id));
                        
            return Ok(_mapper.Map<TaskListResponseShort>(taskList));
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create task list",
            Description = "Creates a list of tasks in the database and returns the ID of the created list from the database",
            Tags = new[] { "Task list" }
            )]
        [SwaggerResponse(StatusCodes.Status201Created, "The task list added to the database", typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Required field not specified")]
        public async Task<ActionResult> AddTaskList([FromBody] TaskListCreateModel taskListModel)
        {
            var addedTaskList = await _sender.Send(new AddTaskListCommandAsync(GetOwner(), _mapper.Map<TaskListDto>(taskListModel)));

            return CreatedAtRoute(nameof(GetTaskListById), new { id = addedTaskList }, addedTaskList);
        }

        [HttpPut()]
        [SwaggerOperation(
            Summary = "Updates the list of tasks by ID",
            Description = "Updates the list of tasks by ID in the database",
            Tags = new[] { "Task list" }
            )]
        [SwaggerResponse(StatusCodes.Status202Accepted, "Updated list of tasks for the specified ID")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The list of tasks for the specified ID was not found")]
        public async Task<ActionResult> UpdateTaskList([FromBody] TaskListUpdateModel taskListModel)
        {
            await _sender.Send(new UpdateTaskListCommandAsync(_mapper.Map<TaskListDto>(taskListModel)));

            return AcceptedAtRoute(nameof(GetTaskListById), new { Id = taskListModel.Id });
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Deletes a task list by the specified Id",
            Description = "Deletes a task list by the specified Id in the database",
            Tags = new[] { "Task list" }
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The task list with the specified Id has been deleted")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The list of tasks for the specified ID was not found")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            await _sender.Send(new DeleteTaskListCommandAsync(id));

            return Ok($"Task list with id = {id} has been removed");
        }

        private string GetOwner()
        {
            return HttpContext.User.Claims.First(c => string.Equals(c.Type, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")).Value;
        }
    }
}
