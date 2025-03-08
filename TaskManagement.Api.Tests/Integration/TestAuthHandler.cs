﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace TaskManagement.Api.Tests.Integration;

public class TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
   ILoggerFactory logger,
   UrlEncoder encoder) :
   AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
   public const string AuthenticationScheme = "Test";

   protected override Task<AuthenticateResult> HandleAuthenticateAsync()
   {
      var claims = new[] { new Claim(ClaimTypes.Name, "Test user") };
      var identity = new ClaimsIdentity(claims, AuthenticationScheme);
      var principal = new ClaimsPrincipal(identity);
      var ticket = new AuthenticationTicket(principal, AuthenticationScheme);
      var result = AuthenticateResult.Success(ticket);

      return Task.FromResult(result);
   }
}
