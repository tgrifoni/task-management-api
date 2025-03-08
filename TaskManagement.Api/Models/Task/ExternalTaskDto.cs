using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Api.Models.Task;

public class ExternalTaskDto
{
   [Required]
   [StringLength(50)]
   public string Title { get; init; } = string.Empty;
   [StringLength(200)]
   public string? Description { get; init; }
   [Required]
   [EnumDataType(typeof(PriorityDto))]
   public PriorityDto Priority { get; init; }
   [Required]
   public DateTime DueDate { get; init; }
   [Required]
   [EnumDataType(typeof(StatusDto))]
   public StatusDto Status { get; init; }
}
