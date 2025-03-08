using TaskManagement.Api.Domain.Contracts.Services;

namespace TaskManagement.Api.Infra.IO.Services;

public class FileWriter : IFileWriter
{
   public void Write(string message, string path, bool append = true)
   {
      using var writer = new StreamWriter(path, append);
      writer.WriteLine(message);
   }

   public async Task WriteAsync(string message, string path, bool append = true)
   {
      using var writer = new StreamWriter(path, append);
      await writer.WriteLineAsync(message);
   }
}
