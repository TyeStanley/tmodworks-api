using Microsoft.AspNetCore.Mvc;
using tmodworks_api.Core.Models;
using tmodworks_api.Core.Interfaces;

namespace tmodworks_api.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TodosController(ITodoService todoService) : ControllerBase
  {
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAll()
    {
      var todos = await todoService.GetAllTodosAsync();
      return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetById(int id)
    {
      var todo = await todoService.GetTodoByIdAsync(id);
      if (todo == null)
      {
        return NotFound();
      }
      return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> Create(Todo todo)
    {
      try
      {
        var createdTodo = await todoService.CreateTodoAsync(todo);
        return CreatedAtAction(nameof(GetById), new { id = createdTodo.Id }, createdTodo);
      }
      catch (ArgumentException ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Todo>> Update(int id, Todo todo)
    {
      try
      {
        var updatedTodo = await todoService.UpdateTodoAsync(id, todo);
        return Ok(updatedTodo);
      }
      catch (ArgumentException ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
      try
      {
        await todoService.DeleteTodoAsync(id);
        return NoContent();
      }
      catch (ArgumentException ex)
      {
        return BadRequest(ex.Message);
      }
    }
  }
}
