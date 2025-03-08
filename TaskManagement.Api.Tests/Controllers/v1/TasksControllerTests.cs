using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManagement.Api.Controllers.v1;
using TaskManagement.Api.Domain.Commands.v1.Task;
using TaskManagement.Api.Models.Task;

namespace TaskManagement.Api.Tests.Controllers.v1;

public class TasksControllerTests
{
   private readonly Mock<IMapper> _mapperMock = new();
   private readonly Mock<IMediator> _mediatorMock = new();
   private readonly TasksController _tasksController;

   public TasksControllerTests()
   {
      _tasksController = new(_mapperMock.Object, _mediatorMock.Object);
   }

   [Fact]
   public async Task Get_WhenRequestIsValid_ReturnsOk()
   {
      _mediatorMock
          .Setup(mediator => mediator.Send(It.IsAny<IRequest<Domain.Queries.v1.Task.GetTasksResponse>>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(It.IsAny<Domain.Queries.v1.Task.GetTasksResponse>());

      var response = new GetTasksResponse(Tasks: [new ExternalTaskDto()]);
      _mapperMock
          .Setup(mapper => mapper.Map<GetTasksResponse>(It.IsAny<Domain.Queries.v1.Task.GetTasksResponse>()))
          .Returns(response);

      var result = await _tasksController.Get() as OkObjectResult;

      Assert.NotNull(result);
      Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
      Assert.Equal(response, result.Value);
   }

   [Fact]
   public async Task GetById_WhenSpecifiedUserDoesNotExist_ReturnsNotFound()
   {
      var response = new Domain.Queries.v1.Task.GetTaskResponse(task: null);
      _mediatorMock
          .Setup(mediator => mediator.Send(It.IsAny<IRequest<Domain.Queries.v1.Task.GetTaskResponse>>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(response);

      var result = await _tasksController.Get(id: default) as ObjectResult;

      Assert.NotNull(result);
      Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
      Assert.IsType<ProblemDetails>(result.Value);
   }

   [Fact]
   public async Task GetById_WhenSpecifiedUserExists_ReturnsOk()
   {
      var response = new Domain.Queries.v1.Task.GetTaskResponse(task: new());
      _mediatorMock
          .Setup(mediator => mediator.Send(It.IsAny<IRequest<Domain.Queries.v1.Task.GetTaskResponse>>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(response);

      var task = new ExternalTaskDto();
      _mapperMock
          .Setup(mapper => mapper.Map<ExternalTaskDto>(response.Task))
          .Returns(task);

      var result = await _tasksController.Get(id: default) as OkObjectResult;

      Assert.NotNull(result);
      Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
      Assert.Equivalent(task, result.Value);
   }

   [Fact]
   public async Task Add_WhenRequestIsValid_ReturnsCreated()
   {
      var request = new CreateTaskRequest { Task = new() };
      _mapperMock
          .Setup(mapper => mapper.Map<AddTaskCommand>(request))
          .Returns(It.IsAny<AddTaskCommand>());

      _mediatorMock
          .Setup(mediator => mediator.Send(It.IsAny<IRequest<int>>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(It.IsAny<int>());

      var result = await _tasksController.Add(request) as CreatedAtActionResult;

      Assert.NotNull(result);
      Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
      Assert.Equal(nameof(TasksController.Add), result.ActionName);
      Assert.Equal(request.Task, result.Value);
   }

   [Fact]
   public async Task Update_WhenIdsDoNotMatch_ReturnsValidationProblem()
   {
      var request = new UpdateTaskRequest { Task = new() };
      _mapperMock
          .Setup(mapper => mapper.Map<UpdateTaskCommand>(request))
          .Returns(It.IsAny<UpdateTaskCommand>());

      var result = await _tasksController.Update(id: -1, request) as ObjectResult;

      Assert.NotNull(result);
      var validationProblemDetails = Assert.IsType<ValidationProblemDetails>(result.Value);
      Assert.Equal("The id provided in the route does not match with the task id.", validationProblemDetails.Detail);
   }

   [Fact]
   public async Task Update_WhenSpecifiedUserDoesNotExist_ReturnsNoContent()
   {
      var request = new UpdateTaskRequest{ Task = new() };
      _mapperMock
          .Setup(mapper => mapper.Map<UpdateTaskCommand>(request))
          .Returns(It.IsAny<UpdateTaskCommand>());

      var response = new Domain.Queries.v1.Task.GetTaskResponse(task: null);
      _mediatorMock
          .Setup(mediator => mediator.Send(It.IsAny<IRequest<Domain.Queries.v1.Task.GetTaskResponse>>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(response);

      var result = await _tasksController.Update(id: request.Task.Id, request) as ObjectResult;

      Assert.NotNull(result);
      Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
      Assert.IsType<ProblemDetails>(result.Value);
   }

   [Fact]
   public async Task Update_WhenRequestIsValid_ReturnsNoContent()
   {
      var request = new UpdateTaskRequest{ Task = new() };
      _mapperMock
          .Setup(mapper => mapper.Map<UpdateTaskCommand>(request))
          .Returns(It.IsAny<UpdateTaskCommand>());

      var response = new Domain.Queries.v1.Task.GetTaskResponse(task: new());
      _mediatorMock
          .Setup(mediator => mediator.Send(It.IsAny<IRequest<Domain.Queries.v1.Task.GetTaskResponse>>(), It.IsAny<CancellationToken>()))
          .ReturnsAsync(response);

      var result = await _tasksController.Update(id: request.Task.Id, request) as NoContentResult;

      Assert.NotNull(result);
      Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
   }

   [Fact]
   public async Task Delete_WhenRequestIsValid_ReturnsNoContent()
   {
      var result = await _tasksController.Delete(id: default) as NoContentResult;

      Assert.NotNull(result);
      Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
   }
}
