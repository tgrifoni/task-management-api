using MediatR;

namespace TaskManagement.Api.Domain.Commands.v1.Task;

public class AddTaskCommand : IRequest<int>
{
   public Models.Entities.Task Task { get; init; } = new();
}
