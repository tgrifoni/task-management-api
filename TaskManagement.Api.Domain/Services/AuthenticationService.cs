using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Api.Domain.Contracts.Services;

namespace TaskManagement.Api.Domain.Services;

public class AuthenticationService(IConfiguration configuration) : IAuthenticationService
{
   public Task<(string Token, bool IsAuthenticated)> Login(string username, string password)
   {
      if (username != "admin" || password != "password")
      {
         return Task.FromResult((Token: string.Empty, IsAuthenticated: false));
      }

      var jwt = GetJwt(username);

      return Task.FromResult((Token: jwt, IsAuthenticated: true));
   }

   private string GetJwt(string username)
   {
      var issuer = configuration["Jwt:Issuer"];
      var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"]!));
      var secondsToExpire = Convert.ToInt32(configuration["Jwt:SecondsToExpire"]);
      var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
      var claims = new ClaimsIdentity(
      [
         new Claim(JwtRegisteredClaimNames.Sub, username),
         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      ]);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
         Audience = issuer,
         Issuer = issuer,
         Expires = DateTime.UtcNow.AddSeconds(secondsToExpire),
         NotBefore = DateTime.UtcNow,
         SigningCredentials = signingCredentials,
         Subject = claims
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
   }
}
