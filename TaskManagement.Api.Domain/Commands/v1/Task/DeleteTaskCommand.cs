namespace TaskManagement.Api.Domain.Commands.v1.Task;

public class DeleteTaskCommand(int id) : AbstractCommand
{
   public int Id { get; private set; } = id;
}
