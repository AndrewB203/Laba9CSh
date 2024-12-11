using Microsoft.AspNetCore.Mvc;
using TodoWebAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private static List<Employee> _employees = new List<Employee>();

    [HttpGet]
    public ActionResult<IEnumerable<Employee>> Get()
    {
        return Ok(_employees);
    }

    [HttpGet("{id}")]
    public ActionResult<Employee> Get(int id)
    {
        var employee = _employees.FirstOrDefault(e => e.ID == id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    [HttpPost]
    public ActionResult<Employee> Post(Employee employee)
    {
        employee.ID = _employees.Count + 1;
        _employees.Add(employee);
        return CreatedAtAction(nameof(Get), new { id = employee.ID }, employee);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, Employee employee)
    {
        var existingEmployee = _employees.FirstOrDefault(e => e.ID == id);
        if (existingEmployee == null)
        {
            return NotFound();
        }
        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.Email = employee.Email;
        existingEmployee.IsActive = employee.IsActive;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var employee = _employees.FirstOrDefault(e => e.ID == id);
        if (employee == null)
        {
            return NotFound();
        }
        _employees.Remove(employee);
        return NoContent();
    }
}
