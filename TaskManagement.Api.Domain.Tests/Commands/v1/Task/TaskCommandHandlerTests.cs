using Moq;
using TaskManagement.Api.Domain.Commands.v1.Task;
using TaskManagement.Api.Domain.Contracts.Repositories;

namespace TaskManagement.Api.Domain.Tests.Commands.v1.Task;

public class TaskCommandHandlerTests
{
   private readonly Mock<ITaskRepository> _taskRepositoryMock = new();
   private readonly TaskCommandHandler _taskCommandHandler;

   public TaskCommandHandlerTests()
   {
      _taskCommandHandler = new TaskCommandHandler(_taskRepositoryMock.Object);
   }

   [Fact]
   public async System.Threading.Tasks.Task AddTaskCommandHandle_WhenCalled_ShouldAddTaskAndReturnId()
   {
      var expectedId = 1;
      _taskRepositoryMock
         .Setup(r => r.AddAsync(It.IsAny<Domain.Models.Entities.Task>()))
         .ReturnsAsync(expectedId);

      var task = Mock.Of<Domain.Models.Entities.Task>();
      var command = new AddTaskCommand { Task = task };

      var actualId = await _taskCommandHandler.Handle(command, It.IsAny<CancellationToken>());

      Assert.Equal(expectedId, actualId);
      _taskRepositoryMock.Verify(repo => repo.AddAsync(task), Times.Once);
   }

   [Fact]
   public async System.Threading.Tasks.Task UpdateTaskCommandHandle_WhenCalled_ShouldUpdateTask()
   {
      var task = Mock.Of<Domain.Models.Entities.Task>();
      var command = new UpdateTaskCommand { Task = task };

      await _taskCommandHandler.Handle(command, It.IsAny<CancellationToken>());

      _taskRepositoryMock.Verify(repo => repo.UpdateAsync(task), Times.Once);
   }

   [Fact]
   public async System.Threading.Tasks.Task DeleteTaskCommandHandle_WhenCalled_ShouldUpdateTask()
   {
      var command = new DeleteTaskCommand(id: default);

      await _taskCommandHandler.Handle(command, It.IsAny<CancellationToken>());

      _taskRepositoryMock.Verify(repo => repo.DeleteAsync(command.Id), Times.Once);
   }
}
