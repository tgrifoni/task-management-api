using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Domain.Contracts.Services;
using TaskManagement.Api.Models.Auth;

namespace TaskManagement.Api.Controllers.v1;

[ApiController]
[Route("[controller]")]
public class LoginController(IAuthenticationService authenticationService) : ControllerBase
{
   [AllowAnonymous]
   [HttpPost]
   public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
   {
      var (token, isAuthenticated) = await authenticationService.Login(request.Username, request.Password);

      if (!isAuthenticated)
      {
         return Unauthorized();
      }

      return Ok(new { token });
   }
}
