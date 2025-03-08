using Moq;
using TaskManagement.Api.Domain.Contracts.Repositories;
using TaskManagement.Api.Domain.Queries.v1.Task;

namespace TaskManagement.Api.Domain.Tests.Queries.v1.Task;

public class TaskQueryHandlerTests
{
   private readonly Mock<ITaskRepository> _taskRepositoryMock = new();
   private readonly TaskQueryHandler _taskQueryHandler;

   public TaskQueryHandlerTests()
   {
      _taskQueryHandler = new TaskQueryHandler(_taskRepositoryMock.Object);
   }

   [Fact]
   public async System.Threading.Tasks.Task GetTaskQueryHandle_WhenCalled_ShouldReturnThatTaskIsNotValid()
   {
      var task = Mock.Of<Domain.Models.Entities.Task>();
      _taskRepositoryMock
         .Setup(repository => repository.GetByIdAsync(task.Id))
         .ReturnsAsync(task);
      var query = new GetTaskQuery(task.Id);

      var response = await _taskQueryHandler.Handle(query, It.IsAny<CancellationToken>());

      Assert.Equal(task, response.Task);
   }

   [Fact]
   public async System.Threading.Tasks.Task GetTasksQueryHandle_WhenCalled_ShouldReturnThatTaskIsNotValid()
   {
      var task = Mock.Of<Domain.Models.Entities.Task>();
      var tasks = new[] { task };
      _taskRepositoryMock
         .Setup(repository => repository.GetAllAsync())
         .ReturnsAsync(tasks);
      var query = new GetTasksQuery();

      var response = await _taskQueryHandler.Handle(query, It.IsAny<CancellationToken>());

      Assert.Equal(tasks, response.Tasks);
   }
}
