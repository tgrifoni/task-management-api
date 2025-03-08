namespace TaskManagement.Api.Domain.Contracts.Repositories;

public interface ICommandRepository<T>
{
   Task<int> AddAsync(T obj);
   Task UpdateAsync(T obj);
   Task DeleteAsync(int id);
}
