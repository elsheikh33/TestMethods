using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMethods.Models;
using TestMethods.Services;

namespace TestMethods.Controllers
{
    [Route("[controller]")]
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
                                                      .Where(c => c.City == city) 
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

        
            [HttpGet("orders-by-customer")]
            public IActionResult GetOrdersByCustomer()
            {
                var service = new OrderService();
                var data = service.GetOrderCounts(5);
                return Ok(data);
            }
        }
    } 

