using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService; 

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("by-price-range")]

    public async Task<IActionResult> GetProductsByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice) {

        var allProducts = await _productService.GetAllProductsAsync();
        var priceList =
         allProducts.Where(p => p.ListPrice >= minPrice && p.ListPrice <= maxPrice) 
         .Select(p => new
         {
             p.ProductId,
             p.ListPrice
         }) 
         .ToList();
        return Ok(priceList);
    }
}