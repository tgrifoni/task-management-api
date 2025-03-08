using Dapper;
using TaskManagement.Api.Domain.Contracts.Repositories;
using TaskManagement.Api.Infra.Data.Connections;

namespace TaskManagement.Api.Infra.Data.Repositories;

public class TaskRepository(IConnectionProvider connectionProvider) : ITaskRepository
{
   private const string _getAllQuery =
      @"SELECT [Id],
               [Title],
               [Description],
               [Priority],
               [DueDate],
               [Status]
          FROM [Task]";
   private const string _getByIdQuery =
      @"SELECT [Id],
               [Title],
               [Description],
               [Priority],
               [DueDate],
               [Status]
          FROM [Task]
         WHERE [Id] = @Id";
   private const string _addCommand =
      @"INSERT INTO [Task]
         (
            [Title],
            [Description],
            [Priority],
            [DueDate],
            [Status]
         ) 
         VALUES
         (
            @Title,
            @Description,
            @Priority,
            @DueDate,
            @Status
         );
         SELECT last_insert_rowid();";
   private const string _updateCommand =
      @"UPDATE [Task]
           SET [Title] = @Title,
               [Description] = @Description,
               [Priority] = @Priority,
               [DueDate] = @DueDate,
               [Status] = @Status
         WHERE [Id] = @Id";
   private const string _deleteCommand = "DELETE FROM [Task] WHERE [Id] = @Id";

   public async Task<IEnumerable<Domain.Models.Entities.Task>> GetAllAsync()
   {
      using var connection = connectionProvider.CreateConnection();
      return await connection.QueryAsync<Domain.Models.Entities.Task>(_getAllQuery);
   }

   public async Task<Domain.Models.Entities.Task?> GetByIdAsync(int id)
   {
      using var connection = connectionProvider.CreateConnection();
      return await connection.QueryFirstOrDefaultAsync<Domain.Models.Entities.Task>(_getByIdQuery, new { Id = id });
   }

   public async Task<int> AddAsync(Domain.Models.Entities.Task task)
   {
      using var connection = connectionProvider.CreateConnection();
      return await connection.ExecuteScalarAsync<int>(_addCommand, task);
   }

   public async Task UpdateAsync(Domain.Models.Entities.Task task)
   {
      using var connection = connectionProvider.CreateConnection();
      await connection.ExecuteAsync(_updateCommand, task);
   }

   public async Task DeleteAsync(int id)
   {
      using var connection = connectionProvider.CreateConnection();
      await connection.ExecuteAsync(_deleteCommand, new { Id = id });
   }
}
