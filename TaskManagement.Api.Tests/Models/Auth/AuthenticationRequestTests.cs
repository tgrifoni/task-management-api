using System.ComponentModel.DataAnnotations;
using TaskManagement.Api.Models.Auth;
using TaskManagement.Api.Models.Task;

namespace TaskManagement.Api.Tests.Models.Auth;

public class AuthenticationRequestTests
{
   [Fact]
   public void AuthenticationRequest_WhenUsernamePropertyIsNull_ShouldReturnValidationError()
   {
      var validationResults = new List<ValidationResult>();
      var model = new AuthenticationRequest
      {
         Username = null!,
         Password = "whatever"
      };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.False(isValid);
      Assert.Contains(validationResults, v =>
         v.MemberNames.Contains("Username") &&
         v.ErrorMessage == "The Username field is required."
      );
   }

   [Fact]
   public void AuthenticationRequest_WhenUsernamePropertyLengthIsLessThanAllowed_ShouldReturnValidationError()
   {
      var validationResults = new List<ValidationResult>();
      var model = new AuthenticationRequest
      {
         Username = "No",
         Password = "whatever"
      };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.False(isValid);
      Assert.Contains(validationResults, v =>
         v.MemberNames.Contains("Username") &&
         v.ErrorMessage == "The field Username must be a string with a minimum length of 5 and a maximum length of 50."
      );
   }

   [Fact]
   public void AuthenticationRequest_WhenUsernamePropertyLengthIsBiggerThanAllowed_ShouldReturnValidationError()
   {
      var validationResults = new List<ValidationResult>();
      var model = new AuthenticationRequest
      {
         Username = "This string contains more than fifty characters for testing purposes.",
         Password = "whatever"
      };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.False(isValid);
      Assert.Contains(validationResults, v =>
         v.MemberNames.Contains("Username") &&
         v.ErrorMessage == "The field Username must be a string with a minimum length of 5 and a maximum length of 50."
      );
   }

   [Fact]
   public void AuthenticationRequest_WhenPasswordPropertyIsNull_ShouldReturnValidationError()
   {
      var validationResults = new List<ValidationResult>();
      var model = new AuthenticationRequest
      {
         Username = "whatever",
         Password = null!
      };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.False(isValid);
      Assert.Contains(validationResults, v =>
         v.MemberNames.Contains("Password") &&
         v.ErrorMessage == "The Password field is required."
      );
   }

   [Fact]
   public void AuthenticationRequest_WhenPasswordPropertyLengthIsLessThanAllowed_ShouldReturnValidationError()
   {
      var validationResults = new List<ValidationResult>();
      var model = new AuthenticationRequest
      {
         Username = "whatever",
         Password = "No"
      };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.False(isValid);
      Assert.Contains(validationResults, v =>
         v.MemberNames.Contains("Password") &&
         v.ErrorMessage == "The field Password must be a string with a minimum length of 5 and a maximum length of 50."
      );
   }

   [Fact]
   public void AuthenticationRequest_WhenPasswordPropertyLengthIsBiggerThanAllowed_ShouldReturnValidationError()
   {
      var validationResults = new List<ValidationResult>();
      var model = new AuthenticationRequest
      {
         Username = "whatever",
         Password = "This string contains more than fifty characters for testing purposes."
      };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.False(isValid);
      Assert.Contains(validationResults, v =>
         v.MemberNames.Contains("Password") &&
         v.ErrorMessage == "The field Password must be a string with a minimum length of 5 and a maximum length of 50."
      );
   }

   [Fact]
   public void AuthenticationRequest_WhenAllPropertiesAreCorrect_ShouldReturnValidationSuccess()
   {
      var validationResults = new List<ValidationResult>();
      var model = new AuthenticationRequest
      {
         Username = "whatever",
         Password = "whatever"
      };
      var validationContext = new ValidationContext(model);

      var isValid = Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

      Assert.True(isValid);
      Assert.Empty(validationResults);
   }
}
