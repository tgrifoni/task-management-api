using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Api.Models.Task;

public record CreateTaskRequest
{
   [Required]
   public ExternalTaskDto Task { get; init; } = new();
}
