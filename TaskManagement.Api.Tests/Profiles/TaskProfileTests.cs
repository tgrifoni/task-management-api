using AutoMapper;
using Moq;
using TaskManagement.Api.Domain.Contracts.Services;
using TaskManagement.Api.Profiles;

namespace TaskManagement.Api.Tests.Profiles;

public class TaskProfileTests
{
   [Fact]
   public void TaskProfile_WhenConfigured_ShouldAssertConfigurationIsValid()
   {
      var eventHandlerMock = new Mock<IHighPriorityTaskEventHandler>();

      var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new TaskProfile(eventHandlerMock.Object)));

      mapperConfiguration.AssertConfigurationIsValid();
   }
}
