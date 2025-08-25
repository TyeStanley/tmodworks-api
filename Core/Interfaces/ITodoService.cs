using tmodworks_api.Core.Models;

namespace tmodworks_api.Core.Interfaces
{
  public interface ITodoService
  {
    Task<IEnumerable<Todo>> GetAllTodosAsync();
    Task<Todo?> GetTodoByIdAsync(int id);
    Task<Todo> CreateTodoAsync(Todo todo);
    Task<Todo> UpdateTodoAsync(int id, Todo todo);
    Task DeleteTodoAsync(int id);
  }
}
