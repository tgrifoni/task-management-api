using AutoMapper;
using TaskManagement.Api.Domain.Commands.v1.Task;
using TaskManagement.Api.Domain.Contracts.Services;
using TaskManagement.Api.Models.Task;
using Task = TaskManagement.Api.Domain.Models.Entities.Task;

namespace TaskManagement.Api.Profiles;

public class TaskProfile : Profile
{
   public TaskProfile(IHighPriorityTaskEventHandler highPriorityTaskEventHandler)
   {
      CreateMap<Task, ExternalTaskDto>();
      CreateMap<Task, InternalTaskDto>();
      CreateMap<InternalTaskDto, Task>()
         .BeforeMap((dto, task) =>
         {
            task.HighPriorityTaskCreated += highPriorityTaskEventHandler.OnHighPriorityTaskCreated!;
            task.HighPriorityTaskUpdated += highPriorityTaskEventHandler.OnHighPriorityTaskUpdated!;
         })
         .AfterMap((dto, task) =>
         {
            task.HighPriorityTaskCreated -= highPriorityTaskEventHandler.OnHighPriorityTaskCreated!;
            task.HighPriorityTaskUpdated -= highPriorityTaskEventHandler.OnHighPriorityTaskUpdated!;
         });
      CreateMap<ExternalTaskDto, Task>()
         .BeforeMap((dto, task) =>
         {
            task.HighPriorityTaskCreated += highPriorityTaskEventHandler.OnHighPriorityTaskCreated!;
            task.HighPriorityTaskUpdated += highPriorityTaskEventHandler.OnHighPriorityTaskUpdated!;
         })
         .ForMember(task => task.Id, opt => opt.Ignore())
         .AfterMap((dto, task) =>
         {
            task.HighPriorityTaskCreated -= highPriorityTaskEventHandler.OnHighPriorityTaskCreated!;
            task.HighPriorityTaskUpdated -= highPriorityTaskEventHandler.OnHighPriorityTaskUpdated!;
         });
      CreateMap<CreateTaskRequest, AddTaskCommand>();
      CreateMap<UpdateTaskRequest, UpdateTaskCommand>();
      CreateMap<Domain.Queries.v1.Task.GetTasksResponse, GetTasksResponse>();
   }
}
