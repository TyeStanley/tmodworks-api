using tmodworks_api.Core.Interfaces;
using tmodworks_api.Core.Models;

namespace tmodworks_api.Core.Services
{
  public class TodoService(ITodoRepository todoRepository) : ITodoService
  {
    public async Task<IEnumerable<Todo>> GetAllTodosAsync()
    {
      return await todoRepository.GetAllAsync();
    }

    public async Task<Todo?> GetTodoByIdAsync(int id)
    {
      return await todoRepository.GetByIdAsync(id);
    }

    public async Task<Todo> CreateTodoAsync(Todo todo)
    {
      // Business logic validation
      if (string.IsNullOrWhiteSpace(todo.Title))
      {
        throw new ArgumentException("Todo title cannot be empty");
      }

      todo.CreatedAt = DateTime.UtcNow;
      return await todoRepository.CreateAsync(todo);
    }

    public async Task<Todo> UpdateTodoAsync(int id, Todo todoUpdate)
    {
      var existingTodo = await todoRepository.GetByIdAsync(id);
      if (existingTodo == null)
      {
        throw new ArgumentException($"Todo with id {id} not found");
      }

      // Business logic validation
      if (string.IsNullOrWhiteSpace(todoUpdate.Title))
      {
        throw new ArgumentException("Todo title cannot be empty");
      }

      existingTodo.Title = todoUpdate.Title;
      existingTodo.Description = todoUpdate.Description;
      existingTodo.DueBy = todoUpdate.DueBy;
      existingTodo.IsComplete = todoUpdate.IsComplete;
      existingTodo.UpdatedAt = DateTime.UtcNow;

      return await todoRepository.UpdateAsync(existingTodo);
    }

    public async Task DeleteTodoAsync(int id)
    {
      var todo = await todoRepository.GetByIdAsync(id);
      if (todo == null)
      {
        throw new ArgumentException($"Todo with id {id} not found");
      }

      await todoRepository.DeleteAsync(id);
    }
  }
}
