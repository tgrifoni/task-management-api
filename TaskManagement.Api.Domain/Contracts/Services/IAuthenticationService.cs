namespace TaskManagement.Api.Domain.Contracts.Services;

public interface IAuthenticationService
{
   Task<(string Token, bool IsAuthenticated)> Login(string username, string password);
}
