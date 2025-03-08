namespace TaskManagement.Api.Domain.Contracts.Services;

public interface IFileWriter
{
   void Write(string message, string path, bool append = true);
   Task WriteAsync(string message, string path, bool append = true);
}
