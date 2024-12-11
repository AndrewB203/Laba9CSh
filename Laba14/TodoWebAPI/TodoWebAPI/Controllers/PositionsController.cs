using Microsoft.AspNetCore.Mvc;
using TodoWebAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
    private static List<Position> _positions = new List<Position>();

    [HttpGet]
    public ActionResult<IEnumerable<Position>> Get()
    {
        return Ok(_positions);
    }

    [HttpGet("{id}")]
    public ActionResult<Position> Get(int id)
    {
        var position = _positions.FirstOrDefault(p => p.ID == id);
        if (position == null)
        {
            return NotFound();
        }
        return Ok(position);
    }

    [HttpPost]
    public ActionResult<Position> Post(Position position)
    {
        position.ID = _positions.Count + 1;
        _positions.Add(position);
        return CreatedAtAction(nameof(Get), new { id = position.ID }, position);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, Position position)
    {
        var existingPosition = _positions.FirstOrDefault(p => p.ID == id);
        if (existingPosition == null)
        {
            return NotFound();
        }
        existingPosition.Title = position.Title;
        existingPosition.Salary = position.Salary;
        existingPosition.IsOpen = position.IsOpen;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var position = _positions.FirstOrDefault(p => p.ID == id);
        if (position == null)
        {
            return NotFound();
        }
        _positions.Remove(position);
        return NoContent();
    }
}