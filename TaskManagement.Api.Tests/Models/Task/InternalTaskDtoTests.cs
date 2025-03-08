using System.ComponentModel.DataAnnotations;
using TaskManagement.Api.Models.Task;

namespace TaskManagement.Api.Tests.Models.Task;

public class InternalTaskDtoTests
{
   [Fact]
   public void InternalTaskRequest_WhenIdPropertyIsOutOfRange_ShouldReturnValidationError()
   {
      var validationResults = new List<ValidationResult>();
      var model = new InternalTaskDto
      {
         Id = -1,
         Title = "title",
         Description = "description",
         Priority = PriorityDto.Low,
         DueDate = DateTime.UtcNow,
         Status = StatusDto.Pending
      };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.False(isValid);
      Assert.Contains(validationResults, v =>
         v.MemberNames.Contains("Id") &&
         v.ErrorMessage == "The field Id must be between 0 and 2147483647."
      );
   }

   [Theory]
   [InlineData(0)]
   [InlineData(int.MaxValue)]
   public void InternalTaskRequest_WhenAllPropertiesAreCorrect_ShouldReturnValidationSuccess(int id)
   {
      var validationResults = new List<ValidationResult>();
      var model = new InternalTaskDto
      {
         Id = id,
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
