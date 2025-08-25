using tmodworks_api.Core.Models;

namespace tmodworks_api.Core.Interfaces
{
  public interface ITodoRepository
  {
    Task<IEnumerable<Todo>> GetAllAsync();
    Task<Todo?> GetByIdAsync(int id);
    Task<Todo> CreateAsync(Todo todo);
    Task<Todo> UpdateAsync(Todo todo);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
  }
}
