using MediatR;
using TaskManagement.Api.Domain.Contracts.Repositories;

namespace TaskManagement.Api.Domain.Queries.v1.Task;

public class TaskQueryHandler(ITaskRepository taskRepository) :
   IRequestHandler<GetTasksQuery, GetTasksResponse>,
   IRequestHandler<GetTaskQuery, GetTaskResponse>
{
   public async Task<GetTaskResponse> Handle(GetTaskQuery request, CancellationToken cancellationToken) =>
      new GetTaskResponse(task: await taskRepository.GetByIdAsync(request.Id));

   public async Task<GetTasksResponse> Handle(GetTasksQuery request, CancellationToken cancellationToken) =>
       new GetTasksResponse(tasks: await taskRepository.GetAllAsync());
}
