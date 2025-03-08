using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManagement.Api.Controllers.v1;
using TaskManagement.Api.Domain.Contracts.Services;
using TaskManagement.Api.Models.Auth;

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
      var request = new AuthenticationRequest
      {
         Username = "whatever",
         Password = "whatever"
      };
      var token = "some token";
      _authenticationServiceMock
         .Setup(s => s.Login(request.Username, request.Password))
         .ReturnsAsync((token, true));

      var result = await _loginController.Login(request) as OkObjectResult;

      Assert.NotNull(result);
      Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
      Assert.Equivalent(new { token }, result.Value);
   }

   [Fact]
   public async Task Login_WhenIsNotAuthenticated_ReturnsUnauthorized()
   {
      var request = new AuthenticationRequest
      {
         Username = "whatever",
         Password = "whatever"
      };
      _authenticationServiceMock
         .Setup(s => s.Login(request.Username, request.Password))
         .ReturnsAsync((string.Empty, false));

      var result = await _loginController.Login(request) as UnauthorizedResult;

      Assert.NotNull(result);
      Assert.Equal(StatusCodes.Status401Unauthorized, result.StatusCode);
   }
}
