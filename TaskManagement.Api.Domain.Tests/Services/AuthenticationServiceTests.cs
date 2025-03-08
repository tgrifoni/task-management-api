using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using TaskManagement.Api.Domain.Contracts.Services;
using TaskManagement.Api.Domain.Services;

namespace TaskManagement.Api.Domain.Tests.Services;

public class AuthenticationServiceTests
{
   private readonly Mock<IConfiguration> _configurationMock = new();
   private readonly IAuthenticationService _authenticationService;

   public AuthenticationServiceTests()
   {
      _authenticationService = new AuthenticationService(_configurationMock.Object);
   }

   [Fact]
   public async Task Handle_WhenUsernameIsIncorrect_ShouldReturnJwtToken()
   {
      (_, bool isAuthenticated) = await _authenticationService.Login(username: "whatever", password: "password");

      Assert.False(isAuthenticated);
   }

   [Fact]
   public async Task Handle_WhenPasswordIsIncorrect_ShouldReturnJwtToken()
   {
      (_, bool isAuthenticated) = await _authenticationService.Login(username: "admin", password: "whatever");

      Assert.False(isAuthenticated);
   }

   [Fact]
   public async Task Handle_WhenUsernameAndPasswordAreCorrect_ShouldReturnJwtToken()
   {
      var issuer = "TestIssuer";
      _configurationMock.SetupGet(configuration => configuration["Jwt:Issuer"]).Returns(issuer);

      var secondsToExpire = 36;
      _configurationMock.SetupGet(configuration => configuration["Jwt:SecondsToExpire"]).Returns($"{secondsToExpire}");

      var key = "---------------- ThisIsATestKey ----------------";
      _configurationMock.SetupGet(configuration => configuration["Jwt:Key"]).Returns(key);

      var username = "admin";
      var password = "password";
      var (token, isAuthenticated) = await _authenticationService.Login(username, password);
      var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

      Assert.True(isAuthenticated);
      Assert.Equal(username, jwtSecurityToken.Claims.Single(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value);
      Assert.Equal(issuer, jwtSecurityToken.Issuer);
   }
}
