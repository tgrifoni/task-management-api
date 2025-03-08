using System.Text;
using TaskManagement.Api.Domain.Models.Enums;

namespace TaskManagement.Api.Domain.Models.Entities;

public class Task
{
   private Priority priority;

   public event EventHandler? HighPriorityTaskCreated;
   public event EventHandler? HighPriorityTaskUpdated;

   bool IsNewTask => Id == 0;
   string Headline => IsNewTask ? "Creating new task" : "Updating task";

   public int Id { get; init; }
   public string Title { get; init; } = string.Empty;
   public string? Description { get; init; }
   public DateTime DueDate { get; init; }
   public Status Status { get; init; }
   public Priority Priority
   {
      get => priority;
      init
      {
         priority = value;
         if (priority is not Priority.High)
         {
            return;
         }

         if (IsNewTask)
         {
            OnHighPriorityTaskCreated(EventArgs.Empty);
            return;
         }

         OnHighPriorityTaskUpdated(EventArgs.Empty);
      }
   }

   protected virtual void OnHighPriorityTaskCreated(EventArgs e)
   {
      HighPriorityTaskCreated?.Invoke(this, e);
   }

   protected virtual void OnHighPriorityTaskUpdated(EventArgs e)
   {
      HighPriorityTaskUpdated?.Invoke(this, e);
   }

   public override string? ToString()
   {
      var sb = new StringBuilder();
      sb
         .AppendLine(Headline)
         .Append("Title: ").AppendLine(Title)
         .Append("Description: ").AppendLine(Description ?? "No description")
         .Append("Due Date: ").AppendLine(DueDate.ToString("o"))
         .Append("Status: ").AppendLine(Status.ToString())
         .Append("Priority: ").AppendLine(Priority.ToString())
         .AppendLine("-----------------------");

      return sb.ToString();
   }
}
