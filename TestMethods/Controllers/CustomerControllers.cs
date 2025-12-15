using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMethods.Models;

namespace TestMethods.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly BikeStoresContext _context;

        public CustomersController(BikeStoresContext context) {
            _context = context;
        }
        [HttpGet("search")]

        public async Task<IActionResult> GetCustomerByCity([FromQuery] string city) 
        {
            var customers = await _context.Customers
                                                      .Where(c => c.City == city) // Filters by city
                                                      .Select(c => new
                                                      {
                                                          c.FirstName,
                                                          c.LastName,
                                                          c.Email,
                                                          c.City
                                                      })
                                                      .ToListAsync();
            if (customers.Count == 0)
            {
                return NotFound($"No customers found in {city}");
            }

            return Ok(customers);
        }
    } 
}
