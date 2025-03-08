namespace TaskManagement.Api.Domain.Queries.v1.Task;

public class GetTaskResponse(Models.Entities.Task? task)
{
   public Models.Entities.Task? Task { get; private set; } = task;
}
