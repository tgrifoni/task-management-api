namespace TaskManagement.Api.Domain.Contracts.Repositories;

public interface ITaskRepository : ICommandQueryRepository<Models.Entities.Task>
{
}
