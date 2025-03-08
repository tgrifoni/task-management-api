using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Api.Domain.Contracts.Services;

namespace TaskManagement.Api.Controllers.v1;

[ApiController]
[Route("[controller]")]
public class LoginController(IAuthenticationService authenticationService) : ControllerBase
{
   [AllowAnonymous]
   [HttpPost]
   public async Task<IActionResult> Login(string username, string password)
   {
      var (token, isAuthenticated) = await authenticationService.Login(username, password);

      if (!isAuthenticated)
      {
         return Unauthorized();
      }

      return Ok(new { token });
   }
}
