using System.Data;

namespace TaskManagement.Api.Infra.Data.Connections;

public interface IConnectionProvider
{
   IDbConnection CreateConnection();
}
