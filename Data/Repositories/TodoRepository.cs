using Microsoft.EntityFrameworkCore;
using tmodworks_api.Core.Interfaces;
using tmodworks_api.Core.Models;

namespace tmodworks_api.Data.Repositories
{
  public class TodoRepository(AppDbContext context) : ITodoRepository
  {
    public async Task<IEnumerable<Todo>> GetAllAsync()
    {
      return await context.Todos.ToListAsync();
    }

    public async Task<Todo?> GetByIdAsync(int id)
    {
      return await context.Todos.FindAsync(id);
    }

    public async Task<Todo> CreateAsync(Todo todo)
    {
      context.Todos.Add(todo);
      await context.SaveChangesAsync();
      return todo;
    }

    public async Task<Todo> UpdateAsync(Todo todo)
    {
      context.Todos.Update(todo);
      await context.SaveChangesAsync();
      return todo;
    }

    public async Task DeleteAsync(int id)
    {
      var todo = await context.Todos.FindAsync(id);
      if (todo != null)
      {
        context.Todos.Remove(todo);
        await context.SaveChangesAsync();
      }
    }

    public async Task<bool> ExistsAsync(int id)
    {
      return await context.Todos.AnyAsync(t => t.Id == id);
    }
  }
}
