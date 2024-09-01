using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GalaxyRestApi.DAL.Models;
using GalaxyRestApi.DAL.Interfaces;

namespace GalaxyRestApi.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IRepository<Car> Cars;

        public CarsController(IRepository<Car> cars)
        {
            Cars = cars;
        }

        [HttpGet("GetCars")]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await Cars.GetAll();
        }

        [HttpGet("GetCarbyId/{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await Cars.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPut("UpdateCar/{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            try
            {
                await Cars.Update(car);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await Cars.Get(id) == null)
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

        [HttpPost("AddNewCar")]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            await Cars.Create(car);

            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }

        [HttpDelete("DeleteCar/{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await Cars.Delete(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }
    }
}
