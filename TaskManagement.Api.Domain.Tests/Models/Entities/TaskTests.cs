using Task = TaskManagement.Api.Domain.Models.Entities.Task;

namespace TaskManagement.Api.Domain.Tests.Models.Entities;

public class TaskTests
{
   [Fact]
   public void ToString_WhenIsNewTask_ShouldReturnCreatingNewTaskHeadline()
   {
      var task = new Task();

      var taskString = task.ToString();

      Assert.Contains("Creating new task", taskString);
   }

   [Fact]
   public void ToString_WhenIsNotNewTask_ShouldReturnUpdatingTaskHeadline()
   {
      var task = new Task { Id = 1 };

      var taskString = task.ToString();

      Assert.Contains("Updating task", taskString);
   }
}
