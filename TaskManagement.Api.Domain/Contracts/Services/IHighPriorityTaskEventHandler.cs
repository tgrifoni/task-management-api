namespace TaskManagement.Api.Domain.Contracts.Services;

public interface IHighPriorityTaskEventHandler
{
   void OnHighPriorityTaskCreated(object sender, EventArgs e);
   void OnHighPriorityTaskUpdated(object sender, EventArgs e);
}
