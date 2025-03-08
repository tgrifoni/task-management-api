using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Api.Models.Task;

public record UpdateTaskRequest
{
   [Required]
   public InternalTaskDto Task { get; init; } = new();
}
