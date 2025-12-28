using Microsoft.EntityFrameworkCore;
using TestMethods.Models;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
}

public class ProductService : IProductService
{
    private readonly BikeStoresContext _context;

    public ProductService(BikeStoresContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

}