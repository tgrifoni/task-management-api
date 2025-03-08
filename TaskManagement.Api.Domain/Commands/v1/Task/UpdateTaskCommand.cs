namespace TaskManagement.Api.Domain.Commands.v1.Task;

public class UpdateTaskCommand : AbstractCommand
{
   public Models.Entities.Task Task { get; init; } = new();
}
