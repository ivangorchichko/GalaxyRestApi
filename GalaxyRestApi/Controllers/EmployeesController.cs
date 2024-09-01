using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GalaxyRestApi.DAL.Models;
using GalaxyRestApi.DAL.Interfaces;

namespace GalaxyRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> Employees;

        public EmployeesController(IRepository<Employee> employees)
        {
            Employees = employees;
        }

        [HttpGet("GetEmployees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await Employees.GetAll();
        }

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await Employees.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPut("UpdateEmployee/{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            try
            {
                await Employees.Update(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await Employees.Get(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("AddNewEmployee")]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            await Employees.Create(employee);

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await Employees.Delete(id);
            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }
    }
}
