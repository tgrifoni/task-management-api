using TaskManagement.Api.Infra.Data.Connections;

namespace TaskManagement.Api;

public class SqliteHostedService(IServiceProvider serviceProvider) : IHostedService
{
   public Task StartAsync(CancellationToken cancellationToken)
   {
      var sqliteConnectionProvider = serviceProvider.GetRequiredService<IConnectionProvider>() as SqliteConnectionProvider;
      sqliteConnectionProvider!.StartDatabase();

      return Task.CompletedTask;
   }

   public Task StopAsync(CancellationToken cancellationToken)
   {
      var sqliteConnectionProvider = serviceProvider.GetRequiredService<IConnectionProvider>() as SqliteConnectionProvider;
      sqliteConnectionProvider!.StopDatabase();

      return Task.CompletedTask;
   }
}
