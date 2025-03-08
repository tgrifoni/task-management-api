using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace TaskManagement.Api.Infra.Data.Connections;

public class SqliteConnectionProvider(IConfiguration configuration) : IConnectionProvider
{
   public void StartDatabase()
   {
      var createTableSqlCommand = @"
         CREATE TABLE IF NOT EXISTS Task
         (
            Id           INTEGER PRIMARY KEY AUTOINCREMENT,
            Title        TEXT    NOT NULL,
            Description  TEXT,
            Priority     INTEGER NOT NULL,
            DueDate      TEXT    NOT NULL,
            Status       INTEGER NOT NULL
         );"
      ;

      using var connection = CreateConnection();
      connection.Execute(createTableSqlCommand);
   }

   public void StopDatabase()
   {
      SqliteConnection.ClearAllPools();

      var dbFileName = configuration["ConnectionStrings:TaskManagementDbName"];
      File.Delete(dbFileName!);
   }

   public IDbConnection CreateConnection() => new SqliteConnection(configuration["ConnectionStrings:TaskManagementDb"]);
}
