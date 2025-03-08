namespace TaskManagement.Api.Domain.Queries.v1.Task;

public class GetTasksResponse(IEnumerable<Models.Entities.Task> tasks)
{
   public IEnumerable<Models.Entities.Task> Tasks { get; private set; } = tasks;
}
