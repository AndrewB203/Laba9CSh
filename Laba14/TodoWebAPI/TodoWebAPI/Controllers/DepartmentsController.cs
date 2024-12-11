using Microsoft.AspNetCore.Mvc;
using TodoWebAPI.Models;


[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private static List<Department> _departments = new List<Department>();

    [HttpGet]
    public ActionResult<IEnumerable<Department>> Get()
    {
        return Ok(_departments);
    }

    [HttpGet("{id}")]
    public ActionResult<Department> Get(int id)
    {
        var department = _departments.FirstOrDefault(d => d.ID == id);
        if (department == null)
        {
            return NotFound();
        }
        return Ok(department);
    }

    [HttpPost]
    public ActionResult<Department> Post(Department department)
    {
        department.ID = _departments.Count + 1;
        _departments.Add(department);
        return CreatedAtAction(nameof(Get), new { id = department.ID }, department);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, Department department)
    {
        var existingDepartment = _departments.FirstOrDefault(d => d.ID == id);
        if (existingDepartment == null)
        {
            return NotFound();
        }
        existingDepartment.Name = department.Name;
        existingDepartment.Description = department.Description;
        existingDepartment.IsActive = department.IsActive;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var department = _departments.FirstOrDefault(d => d.ID == id);
        if (department == null)
        {
            return NotFound();
        }
        _departments.Remove(department);
        return NoContent();
    }
}

