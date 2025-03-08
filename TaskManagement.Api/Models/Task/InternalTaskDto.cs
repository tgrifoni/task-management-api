using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Api.Models.Task;

public class InternalTaskDto : ExternalTaskDto
{
   [Key]
   [Range(0, int.MaxValue)]
   public int Id { get; init; }
}
