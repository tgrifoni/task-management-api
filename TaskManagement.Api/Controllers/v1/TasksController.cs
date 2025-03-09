using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TaskManagement.Api.Domain.Commands.v1.Task;
using TaskManagement.Api.Domain.Queries.v1.Task;
using TaskManagement.Api.Models.Task;

namespace TaskManagement.Api.Controllers.v1;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TasksController(IMapper mapper, IMediator mediator) : ControllerBase
{
   [HttpGet]
   [ProducesResponseType<Models.Task.GetTasksResponse>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
   public async Task<IActionResult> Get()
   {
      var response = await mediator.Send(new GetTasksQuery());
      var getTasksResponse = mapper.Map<Models.Task.GetTasksResponse>(response);

      return Ok(getTasksResponse);
   }

   [HttpGet("{id}")]
   [ProducesResponseType<InternalTaskDto>(StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
   [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)]
   public async Task<IActionResult> Get(int id)
   {
      var response = await mediator.Send(new GetTaskQuery(id));
      if (response.Task is null)
      {
         return Problem(detail: "The specified user could not be found.", statusCode: StatusCodes.Status404NotFound);
      }

      var task = mapper.Map<InternalTaskDto>(response.Task);
      return Ok(task);
   }

   [HttpPost]
   [ProducesResponseType<ExternalTaskDto>(StatusCodes.Status201Created, MediaTypeNames.Application.Json)]
   [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)]
   public async Task<IActionResult> Add([FromBody] CreateTaskRequest request)
   {
      var command = mapper.Map<AddTaskCommand>(request);
      var id = await mediator.Send(command);
      
      return CreatedAtAction(nameof(Add), new { id }, request.Task);
   }

   [HttpPut("{id}")]
   [ProducesResponseType(StatusCodes.Status204NoContent)]
   [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)]
   [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, MediaTypeNames.Application.Json)]
   public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskRequest request)
   {
      if (id != request.Task.Id)
         return ValidationProblem("The id provided in the route does not match with the task id.");

      var getTaskResponse = await mediator.Send(new GetTaskQuery(id));
      if (getTaskResponse.Task is null)
      {
         return Problem(detail: "The specified user could not be found.", statusCode: StatusCodes.Status404NotFound);
      }

      var command = mapper.Map<UpdateTaskCommand>(request);
      await mediator.Send(command);

      return NoContent();
   }

   [HttpDelete("{id}")]
   [ProducesResponseType(StatusCodes.Status204NoContent)]
   public async Task<IActionResult> Delete(int id)
   {
      var command = new DeleteTaskCommand(id);
      await mediator.Send(command);

      return NoContent();
   }
}