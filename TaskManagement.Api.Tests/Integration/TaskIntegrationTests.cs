using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskManagement.Api.Models.Task;

namespace TaskManagement.Api.Tests.Integration;

public class TaskIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
   private readonly HttpClient _client;
   private readonly JsonSerializerOptions _jsonSerializerOptions = new()
   {
      PropertyNameCaseInsensitive = true,
      Converters = { new JsonStringEnumConverter() }
   };

   public TaskIntegrationTests(WebApplicationFactory<Program> factory)
   {
      _client = factory
         .WithWebHostBuilder(builder => builder
            .UseEnvironment("Integration")
            .ConfigureTestServices(services =>
            {
               services.AddControllers().AddDataAnnotationsLocalization();
               services
                  .AddAuthentication(TestAuthHandler.AuthenticationScheme)
                  .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme, options => { });
            })
            .ConfigureAppConfiguration((context, config) => config.AddJsonFile("appsettings.json")))
         .CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
   }

   [Fact]
   public async Task Add_WhenRequestIsValid_ShouldSaveAndReturnCreated()
   {
      var request = new CreateTaskRequest
      {
         Task = new()
         {
            Title = "Title",
            Description = "Description",
            DueDate = DateTime.UtcNow,
            Status = StatusDto.Pending,
            Priority = PriorityDto.Low
         }
      };

      var httpResponse = await _client.PostAsync("/tasks", JsonContent.Create(request));
      var json = await httpResponse.Content.ReadAsStringAsync();
      var response = JsonSerializer.Deserialize<ExternalTaskDto>(json, _jsonSerializerOptions);

      httpResponse.EnsureSuccessStatusCode();
      Assert.Equal(System.Net.HttpStatusCode.Created, httpResponse.StatusCode);
      Assert.Equal("http://localhost/Tasks?id=1", httpResponse.Headers.Location!.ToString());
      Assert.NotNull(response);
      Assert.Equal(request.Task.Title, response.Title);
      Assert.Equal(request.Task.Description, response.Description);
      Assert.Equal(request.Task.DueDate, response.DueDate);
      Assert.Equal(request.Task.Status, response.Status);
      Assert.Equal(request.Task.Priority, response.Priority);
   }
}
