using TaskManagement.Api.Domain.Contracts.Services;

namespace TaskManagement.Api.Domain.Services;

public class HighPriorityTaskEventHandler(IFileWriter fileWriter) : IHighPriorityTaskEventHandler
{
   private const string Path = "critical-updates.txt";

   public void OnHighPriorityTaskCreated(object sender, EventArgs e)
   {
      LogCriticalUpdate(sender);
   }

   public void OnHighPriorityTaskUpdated(object sender, EventArgs e)
   {
      LogCriticalUpdate(sender);
   }

   private void LogCriticalUpdate(object sender)
   {
      var message = sender.ToString();
      fileWriter.Write(message!, Path);
   }
}
