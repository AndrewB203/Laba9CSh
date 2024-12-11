using Microsoft.AspNetCore.Mvc;
using TodoWebAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class TodoItemsController : ControllerBase
{
    private static List<TodoItem> _todoItems = new List<TodoItem>();

    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> Get()
    {
        return Ok(_todoItems);
    }

    [HttpGet("{id}")]
    public ActionResult<TodoItem> Get(int id)
    {
        var todoItem = _todoItems.FirstOrDefault(t => t.ID == id);
        if (todoItem == null)
        {
            return NotFound();
        }
        return Ok(todoItem);
    }

    [HttpPost]
    public ActionResult<TodoItem> Post(TodoItem todoItem)
    {
        todoItem.ID = _todoItems.Count + 1;
        _todoItems.Add(todoItem);
        return CreatedAtAction(nameof(Get), new { id = todoItem.ID }, todoItem);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, TodoItem todoItem)
    {
        var existingTodoItem = _todoItems.FirstOrDefault(t => t.ID == id);
        if (existingTodoItem == null)
        {
            return NotFound();
        }
        existingTodoItem.Name = todoItem.Name;
        existingTodoItem.Notes = todoItem.Notes;
        existingTodoItem.Done = todoItem.Done;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var todoItem = _todoItems.FirstOrDefault(t => t.ID == id);
        if (todoItem == null)
        {
            return NotFound();
        }
        _todoItems.Remove(todoItem);
        return NoContent();
    }
}
