using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMethods.Models;

namespace TestMethods.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly BikeStoresContext _context;
        public BrandsController(BikeStoresContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> addBrand([FromBody] string brandName)
        {
            var newBrand = new Brand
            { 
                BrandName = brandName
            };
            _context.Brands.Add(newBrand);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Brand added successfully", id = newBrand.BrandId });
        }
        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _context.Brands.OrderBy(b => b.BrandName)
                .ToListAsync();
            return Ok(brands);

        }
    }
}
 


