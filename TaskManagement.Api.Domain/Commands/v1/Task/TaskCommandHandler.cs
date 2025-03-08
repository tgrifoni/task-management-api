using MediatR;
using TaskManagement.Api.Domain.Contracts.Repositories;

namespace TaskManagement.Api.Domain.Commands.v1.Task;

public class TaskCommandHandler(ITaskRepository taskRepository) :
   IRequestHandler<AddTaskCommand, int>,
   IRequestHandler<DeleteTaskCommand>,
   IRequestHandler<UpdateTaskCommand>
{
   public async Task<int> Handle(AddTaskCommand command, CancellationToken cancellationToken) =>
      await taskRepository.AddAsync(command.Task);

   public async System.Threading.Tasks.Task Handle(UpdateTaskCommand command, CancellationToken cancellationToken) =>
      await taskRepository.UpdateAsync(command.Task);

   public async System.Threading.Tasks.Task Handle(DeleteTaskCommand command, CancellationToken cancellationToken) =>
      await taskRepository.DeleteAsync(command.Id);
}
