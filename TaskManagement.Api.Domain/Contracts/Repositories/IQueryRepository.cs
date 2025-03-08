namespace TaskManagement.Api.Domain.Contracts.Repositories;

public interface IQueryRepository<T>
{
   Task<IEnumerable<T>> GetAllAsync();
   Task<T?> GetByIdAsync(int id);
}
