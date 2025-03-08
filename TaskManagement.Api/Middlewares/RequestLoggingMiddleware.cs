using TaskManagement.Api.Domain.Contracts.Services;

namespace TaskManagement.Api.Middlewares;

public class RequestLoggingMiddleware(RequestDelegate next)
{
   public async Task InvokeAsync(HttpContext context, IFileWriter fileWriter)
   {
      var method = context.Request.Method;
      var endpoint = context.Request.Path;
      var message = $"{method} {endpoint}";
      var path = "logs.txt";

      await fileWriter.WriteAsync(message, path);

      await next(context);
   }
}
