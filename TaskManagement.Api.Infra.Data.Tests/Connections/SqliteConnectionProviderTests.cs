using Microsoft.Extensions.Configuration;
using Moq;
using TaskManagement.Api.Infra.Data.Connections;

namespace TaskManagement.Api.Infra.Data.Tests.Connections;

public class SqliteConnectionProviderTests
{
   private const string ConnectionString = "Data Source=:memory:";

   private readonly Mock<IConfiguration> _configurationMock = new();
   private readonly IConnectionProvider _connectionProvider;

   public SqliteConnectionProviderTests()
   {
      _configurationMock.SetupGet(configuration => configuration[It.IsAny<string>()]).Returns(ConnectionString);
      _connectionProvider = new SqliteConnectionProvider(_configurationMock.Object);
   }

   [Fact]
   public void CreateConnection_WhenCalled_ReturnsSqliteConnection()
   {
      using var connection = _connectionProvider.CreateConnection();

      Assert.NotNull(connection);
      Assert.Equal(ConnectionString, connection.ConnectionString);
   }
}
