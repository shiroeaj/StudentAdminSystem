using StudentAdminSystem.Data;
using StudentAdminSystem.Models;
using MongoDB.Driver;
using System.Collections.Generic; // Required for List<T>
using System.Threading.Tasks;     // Required for Task

namespace StudentAdminSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly MongoDbContext _context;

        public ProductService(MongoDbContext context)
        {
            _context = context;
        }

        // FIXED: Syntax was 'Task>', changed to 'Task<List<Product>>'
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Find(p => p.IsActive).ToListAsync();
        }

        // FIXED: Changed 'Task' to 'Task<Product>' to match Interface
        public async Task<Product> GetProductByIdAsync(string productId)
        {
            return await _context.Products
                .Find(p => p.Id == productId && p.IsActive)
                .FirstOrDefaultAsync();
        }

        // FIXED: Syntax was 'Task>', changed to 'Task<List<Product>>'
        public async Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            return await _context.Products
                .Find(p => p.Category == category && p.IsActive)
                .ToListAsync();
        }

        // FIXED: Changed 'Task' to 'Task<Product>' to match Interface and return statement
        public async Task<Product> CreateProductAsync(Product product)
        {
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        // FIXED: Changed 'Task' to 'Task<Product>' to match Interface and return statement
        public async Task<Product> UpdateProductAsync(string productId, Product product)
        {
            product.UpdatedAt = DateTime.UtcNow;
            await _context.Products.ReplaceOneAsync(p => p.Id == productId, product);
            return product;
        }

        // FIXED: Changed 'Task' to 'Task<bool>' to match Interface
        public async Task<bool> DeleteProductAsync(string productId)
        {
            var result = await _context.Products.DeleteOneAsync(p => p.Id == productId);
            return result.DeletedCount > 0;
        }
    }
}