using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManagement.Api.Controllers.v1;
using TaskManagement.Api.Domain.Contracts.Services;

namespace TaskManagement.Api.Tests.Controllers.v1;

public class LoginControllerTests
{
   private readonly Mock<IAuthenticationService> _authenticationServiceMock = new();
   private readonly LoginController _loginController;

   public LoginControllerTests()
   {
      _loginController = new(_authenticationServiceMock.Object);
   }

   [Fact]
   public async Task Login_WhenIsAuthenticated_ReturnsOk()
   {
      var username = "username";
      var password = "password";
      var token = "some token";
      _authenticationServiceMock
         .Setup(s => s.Login(username, password))
         .ReturnsAsync((token, true));

      var result = await _loginController.Login(username, password) as OkObjectResult;

      Assert.NotNull(result);
      Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
      Assert.Equivalent(new { token }, result.Value);
   }

   [Fact]
   public async Task Login_WhenIsNotAuthenticated_ReturnsUnauthorized()
   {
      var username = "username";
      var password = "password";
      _authenticationServiceMock
         .Setup(s => s.Login(username, password))
         .ReturnsAsync((string.Empty, false));

      var result = await _loginController.Login(username, password) as UnauthorizedResult;

      Assert.NotNull(result);
      Assert.Equal(StatusCodes.Status401Unauthorized, result.StatusCode);
   }
}
