namespace TaskManagement.Api.Models.Task;

public record GetTasksResponse(IEnumerable<InternalTaskDto> Tasks);
