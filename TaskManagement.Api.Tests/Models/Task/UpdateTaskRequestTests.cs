using Moq;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Api.Models.Task;

namespace TaskManagement.Api.Tests.Models.Task;

public class UpdateTaskRequestTests
{
   [Fact]
   public void UpdateTaskRequest_WhenTaskPropertyIsNull_ShouldReturnValidationError()
   {
      var validationResults = new List<ValidationResult>();
      var model = new UpdateTaskRequest { Task = It.IsAny<InternalTaskDto>() };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.False(isValid);
      Assert.Contains(validationResults, v =>
         v.MemberNames.Contains("Task") &&
         v.ErrorMessage == "The Task field is required."
      );
   }

   [Fact]
   public void UpdateTaskRequest_WhenTaskPropertyIsNotNull_ShouldReturnValidationSuccess()
   {
      var validationResults = new List<ValidationResult>();
      var model = new UpdateTaskRequest{ Task = new InternalTaskDto() };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.True(isValid);
      Assert.Empty(validationResults);
   }
}
