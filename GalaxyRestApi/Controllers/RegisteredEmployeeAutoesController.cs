using Microsoft.AspNetCore.Mvc;
using GalaxyRestApi.DAL.Models;
using GalaxyRestApi.DAL.Interfaces;

namespace GalaxyRestApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class RegisteredEmployeeAutoesController : ControllerBase
    {
        private readonly IRepository<Car> Cars;
        private readonly IRepository<Employee> Employees;
        private readonly IRepository<RegisteredEmployeeAuto> RegisteredEmployees;

        public RegisteredEmployeeAutoesController(IRepository<Car> cars, 
            IRepository<Employee> employees, 
            IRepository<RegisteredEmployeeAuto> registeredEmployees)
        {
            Cars = cars;
            Employees = employees;
            RegisteredEmployees = registeredEmployees;
        }

        [HttpGet("CheckAutoRegistration/{CarNumber}")]
        public async Task<ActionResult<RegisteredEmployeeAuto>> CheckAutoRegistration(string CarNumber)
        {
            var cars = await Cars.GetAll();
            var registeredAutos = await RegisteredEmployees.GetAll();

            if(cars != null && registeredAutos != null) {
                var car = cars.FirstOrDefault(c => c.CarNumber == CarNumber);

                if (car == null) { return NotFound(); }

                var registeredEmployeeAuto = registeredAutos.FirstOrDefault(c => c.CarId == car.Id);

                if (registeredEmployeeAuto == null)
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost("RegisterEmployeeCar/{EmployeeName}/{CarNumber}")]
        public async Task<ActionResult<RegisteredEmployeeAuto>> RegisterEmployeeCar(string EmployeeName, string CarNumber)
        {
            var cars = await Cars.GetAll();
            var registeredAutos = await RegisteredEmployees.GetAll();
            var employees = await Employees.GetAll();

            if (cars != null && registeredAutos != null && employees != null)
            {
                var car = cars.FirstOrDefault(c => c.CarNumber == CarNumber);
                var employee = employees.FirstOrDefault(e => e.Name == EmployeeName);

                if (employee == null || car == null)
                {
                    NotFound();
                }
                else
                {
                    var registeredEmployee = new RegisteredEmployeeAuto()
                    {
                        Id = registeredAutos.Count + 1,
                        Car = car,
                        CarId = car.Id,
                        Employee = employee,
                        EmployeeId = employee.Id
                    };

                    await RegisteredEmployees.Create(registeredEmployee);

                    return CreatedAtAction(nameof(RegisterEmployeeCar), new { id = registeredEmployee.Id }, registeredEmployee);
                }

            }

            return Ok();
        }
    }
}
