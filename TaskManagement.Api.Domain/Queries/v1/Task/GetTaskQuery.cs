namespace TaskManagement.Api.Domain.Queries.v1.Task;

public class GetTaskQuery(int id) : AbstractQuery<GetTaskResponse>
{
   public int Id { get; private set; } = id;
}
