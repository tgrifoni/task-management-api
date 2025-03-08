using System.ComponentModel.DataAnnotations;
using TaskManagement.Api.Models.Task;

namespace TaskManagement.Api.Tests.Models.Task;

public class ExternalTaskDtoTests
{
   [Fact]
   public void ExternalTaskRequest_WhenTitlePropertyIsNull_ShouldReturnValidationError()
   {
      var validationResults = new List<ValidationResult>();
      var model = new ExternalTaskDto
      {
         Description = "description",
         Priority = PriorityDto.Low,
         DueDate = DateTime.UtcNow,
         Status = StatusDto.Pending
      };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.False(isValid);
      Assert.Contains(validationResults, v =>
         v.MemberNames.Contains("Title") &&
         v.ErrorMessage == "The Title field is required."
      );
   }

   [Fact]
   public void ExternalTaskRequest_WhenTitlePropertyLengthIsBiggerThanAllowed_ShouldReturnValidationError()
   {
      var validationResults = new List<ValidationResult>();
      var model = new ExternalTaskDto
      {
         Title = "This string contains more than fifty characters for testing purposes.",
         Description = "description",
         Priority = PriorityDto.Low,
         DueDate = DateTime.UtcNow,
         Status = StatusDto.Pending
      };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.False(isValid);
      Assert.Contains(validationResults, v =>
         v.MemberNames.Contains("Title") &&
         v.ErrorMessage == "The field Title must be a string with a maximum length of 50."
      );
   }

   [Fact]
   public void ExternalTaskRequest_WhenDescriptionPropertyLengthIsBiggerThanAllowed_ShouldReturnValidationError()
   {
      var validationResults = new List<ValidationResult>();
      var model = new ExternalTaskDto
      {
         Title = "title",
         Description = "This string property contains more than the two hundred characters allowed for testing purposes. " +
            "This is required a very long string, so we will probably break it down on multiple lines, just to facilitate readability.",
         Priority = PriorityDto.Low,
         DueDate = DateTime.UtcNow,
         Status = StatusDto.Pending
      };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.False(isValid);
      Assert.Contains(validationResults, v =>
         v.MemberNames.Contains("Description") &&
         v.ErrorMessage == "The field Description must be a string with a maximum length of 200."
      );
   }

   [Fact]
   public void ExternalTaskRequest_WhenAllPropertiesAreCorrect_ShouldReturnValidationSuccess()
   {
      var validationResults = new List<ValidationResult>();
      var model = new ExternalTaskDto
      {
         Title = "title",
         Description = "description",
         Priority = PriorityDto.Low,
         DueDate = DateTime.UtcNow,
         Status = StatusDto.Pending
      };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.True(isValid);
      Assert.Empty(validationResults);
   }
}
