using Moq;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Api.Models.Task;

namespace TaskManagement.Api.Tests.Models.Task;

public class CreateTaskRequestTests
{
   [Fact]
   public void CreateTaskRequest_WhenTaskPropertyIsNull_ShouldReturnValidationError()
   {
      var validationResults = new List<ValidationResult>();
      var model = new CreateTaskRequest{ Task = It.IsAny<ExternalTaskDto>() };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.False(isValid);
      Assert.Contains(validationResults, v =>
         v.MemberNames.Contains("Task") &&
         v.ErrorMessage == "The Task field is required."
      );
   }

   [Fact]
   public void CreateTaskRequest_WhenTaskPropertyIsNotNull_ShouldReturnValidationSuccess()
   {
      var validationResults = new List<ValidationResult>();
      var model = new CreateTaskRequest{ Task = new ExternalTaskDto() };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.True(isValid);
      Assert.Empty(validationResults);
   }
}
